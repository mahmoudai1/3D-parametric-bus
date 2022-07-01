using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _08_Step
{
    public partial class Form1 : Form
    {
        int XB = 0;
        int YB = 0;
        int cx = 800;
        int cy = 800;

        Bitmap off;

        Camera cam = new Camera();
        Parmetric_Model bus = new Parmetric_Model();

        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.Paint += new PaintEventHandler(Form1_Paint);
            this.Load += new EventHandler(Form1_Load);
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
        }

        void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            double cs;
            double sn;
            double zn;
            double xn;
            double yn;
            double dx, dy, dz;

            cs = Math.Cos(1 * Math.PI / 180);
            sn = Math.Sin(1 * Math.PI / 180);

            switch (e.KeyCode)
            {
                case Keys.Up:
                    dx = cam.lookAt.X - cam.cop.X;
                    dy = cam.lookAt.Y - cam.cop.Y;
                    dz = cam.lookAt.Z - cam.cop.Z;
                    cam.cop.X += (float)dx * 0.01f;
                    cam.cop.Y += (float)dy * 0.01f;
                    cam.cop.Z += (float)dz * 0.01f;

                    //cam.cop.Z++;              
                    break;
                case Keys.Down:
                    dx = cam.lookAt.X - cam.cop.X;
                    dy = cam.lookAt.Y - cam.cop.Y;
                    dz = cam.lookAt.Z - cam.cop.Z;
                    cam.cop.X -= (float)dx * 0.01f;
                    cam.cop.Y -= (float)dy * 0.01f;
                    cam.cop.Z -= (float)dz * 0.01f;
                    break;

                case Keys.Right:
                    zn = cam.cop.Z * cs - cam.cop.X * sn;
                    xn = cam.cop.Z * sn + cam.cop.X * cs;
                    cam.cop.X = (float)xn;
                    cam.cop.Z = (float)zn;
                    cam.BuildNewSystem();

                    break;
                case Keys.Left:
                    zn = cam.cop.Z * cs - cam.cop.X * -sn;
                    xn = cam.cop.Z * -sn + cam.cop.X * cs;
                    cam.cop.X = (float)xn;
                    cam.cop.Z = (float)zn;
                    cam.BuildNewSystem();
                    break;

                case Keys.Y:
                    yn = cam.cop.Y * cs - cam.cop.Z * sn;
                    zn = cam.cop.Y * sn + cam.cop.Z * cs;
                    cam.cop.Y = (float)yn;
                    cam.cop.Z = (float)zn;
                    cam.BuildNewSystem();
                    break;
                case Keys.H:
                    yn = cam.cop.Y * cs - cam.cop.Z * -sn;
                    zn = cam.cop.Y * -sn + cam.cop.Z * cs;
                    cam.cop.Y = (float)yn;
                    cam.cop.Z = (float)zn;
                    cam.BuildNewSystem();
                    break;

                case Keys.Space:
                    //CPoint3D_Node p1 = new CPoint3D_Node(bus.L_3D_Pts[bus.L_Edges[0]]);
                    //CPoint3D_Node p2 = new CPoint3D_Node(bus.L_3D_Pts[bus.L_Edges[0]]);
                    //Transformation_API.RotateArbitrary(ref bus.L_3D_Pts, p1, p2, 1);

                    cam.BuildNewSystem();
                    break;

            }
            cam.BuildNewSystem();        

            drawDouble(this.CreateGraphics());
        }

        void Form1_Load(object sender, EventArgs e)
        {
            off = new Bitmap(this.Width, this.Height);

            cam.ceneterX = 350;
            cam.ceneterY = 400;
            cam.cxScreen = cx;
            cam.cyScreen = cy;
            cam.cop.Y = 300;

            cam.BuildNewSystem();

            bus.cam = cam;
            bus.Design();
        }

        void Form1_Paint(object sender, PaintEventArgs e)
        {
            drawDouble(e.Graphics);
        }

        void drawScene(Graphics g)
        {
            g.Clear(Color.Gray);

            Pen P = new Pen(Color.Black, 5);
            g.DrawRectangle(P, XB, YB, cx - 46, cy);

            bus.DrawYourSelf(g);

        }

        public void drawDouble(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(off);
            drawScene(g2);
            g.DrawImage(off, 0, 0);
        }
    }
}
