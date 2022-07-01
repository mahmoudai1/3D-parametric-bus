using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections;
using System.IO;
using System.Drawing;

namespace _08_Step
{
    class _3D_Model
    {
        public ArrayList L_3D_Pts = new ArrayList();
        public ArrayList L_Edges = new ArrayList();

        public float focal = 0.5f;

        public Camera cam;

        public Color clr = Color.Black;

        public void AddPoint(CPoint3D_Node pnn)
        {
            L_3D_Pts.Add(pnn);
        }

        public void AddEdge(int i, int j, Color cl)
        {
            Edge pnn = new Edge(i, j);
            pnn.cl = cl;
            L_Edges.Add(pnn);
        }

        public void LoadFromFile(string strF)
        {

            L_3D_Pts = new ArrayList();
            L_Edges = new ArrayList();
            StreamReader sr = new StreamReader( strF );

            string strLine;
            int Flag = 0;
            while ( (strLine = sr.ReadLine())   != null)
            {
                if (strLine[0] == 'L')
                {
                    Flag = 1;
                    continue;
                }

                if (Flag == 0)
                {
                    string []ss = strLine.Split(',');
                    CPoint3D_Node pnn = new CPoint3D_Node( 
                                float.Parse (ss[0].Trim()),
                                float.Parse(ss[1].Trim()),
                                float.Parse(ss[2].Trim())
                                );

                    L_3D_Pts.Add(pnn);
                }

                if (Flag == 1)
                {
                    string[] ss = strLine.Split(',');
                    Edge pnn = new Edge(
                                int.Parse(ss[0].Trim()),
                                int.Parse(ss[1].Trim())
                                );

                    L_Edges.Add(pnn);
                }

            }

            sr.Close();
        }

        public void DrawYourSelf(Graphics g)
        {            
            

            CPoint3D_Node tmp1 ;
            CPoint3D_Node tmp2 ;

            for (int i = 0; i < L_Edges.Count; i++)
            {
                Edge ptrv = (Edge)L_Edges[i];
                Pen PP = new Pen(ptrv.cl);

                tmp1 = new CPoint3D_Node((CPoint3D_Node)L_3D_Pts[ptrv.E0]);
                tmp2 = new CPoint3D_Node((CPoint3D_Node)L_3D_Pts[ptrv.E1]);

                bool isVisible = cam.TransformToOrigin_And_Rotate_And_Project( tmp1, tmp2);                                                                                
                //PointF e = cam.TransformToOrigin_And_Rotate_And_Project((CPoint3D_Node)L_3D_Pts[ptrv.E1]);
                if (isVisible)
                    g.DrawLine( PP, 
                                tmp1.X , 
                                tmp1.Y ,
                                tmp2.X ,
                                tmp2.Y );
            }

            //Font FF = new Font("System", 10);
            //for (int i = 0; i < L_2D.Count; i++)
            //{
            //    PointF v = (PointF)L_2D[i];
            //    g.FillEllipse(Brushes.Red,
            //                    XB + v.X - 5,
            //                    YB + v.Y - 5,
            //                    10, 10);
            //    g.DrawString("P" + (i), FF, Brushes.Green, XB + v.X, YB + v.Y + 10);
            //}
        }

        
        public void Rotat_Aroun_Edge(int iEdge)
        {
            if (iEdge < 0 || iEdge >= L_Edges.Count)
                return;

                Edge e = (Edge)L_Edges[iEdge];
                CPoint3D_Node pointer = (CPoint3D_Node )L_3D_Pts[e.E0];
                CPoint3D_Node pointer2 = (CPoint3D_Node)L_3D_Pts[e.E1];

                CPoint3D_Node v1 = new CPoint3D_Node(pointer.X,pointer .Y, pointer .Z) ;
                CPoint3D_Node v2 = new CPoint3D_Node(pointer2.X, pointer2.Y, pointer2.Z); 

                Transformation_API.RotateArbitrary(
                                            ref L_3D_Pts,
                                            v1, //(CPoint3D_Node)L_3D_Pts[0],
                                            v2, //(CPoint3D_Node)L_3D_Pts[1],
                                            5);
        }
    
    }
}
