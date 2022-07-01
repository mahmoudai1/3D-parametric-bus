using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace _08_Step
{
    class Camera
    {
        public CPoint3D_Node cop;
        public CPoint3D_Node lookAt;
        public CPoint3D_Node up;
        public float  front, back;

        public float focal = 0.5f;

        public float tanH, tanV;

        // vectors
        public CPoint3D_Node basisa, lookDir, basisc;

        public int ceneterX, ceneterY;
        public int cxScreen, cyScreen;

        public Camera()
        {
            cop = new CPoint3D_Node(0, 0, -450); // new Point3D(0, -50, 0);
            lookAt = new CPoint3D_Node(0, 0, 1);     //new Point3D(0, 50, 0);
            up = new CPoint3D_Node(0, 1, 0);
            front = 10; // 70.0;
            back = 5200.0f;

            tanH = (float)(Math.Tan(45 / 2 * Math.PI / 180));
            tanV = (float)(Math.Tan(45 / 2 * Math.PI / 180));
        }

        public void BuildNewSystem()
        {
            lookDir = new CPoint3D_Node(0, 0, 0);
            basisa = new CPoint3D_Node(0, 0, 0);
            basisc = new CPoint3D_Node(0, 0, 0);

            lookDir.X = lookAt.X - cop.X;
            lookDir.Y = lookAt.Y - cop.Y;
            lookDir.Z = lookAt.Z - cop.Z;
            Matrix.Normalise(lookDir);

            basisa = Matrix.CrossProduct(up, lookDir);
            Matrix.Normalise(basisa);

            basisc = Matrix.CrossProduct(lookDir, basisa);
            Matrix.Normalise(basisc);
        }

        public void TransformToOrigin_And_Rotate(CPoint3D_Node a, CPoint3D_Node e)
        {
            CPoint3D_Node w = new CPoint3D_Node(a.X , a.Y , a.Z);
            w.X -= cop.X;
            w.Y -= cop.Y;
            w.Z -= cop.Z;

            e.X = w.X * basisa.X + w.Y * basisa.Y + w.Z * basisa.Z;
            e.Y = w.X * basisc.X + w.Y * basisc.Y + w.Z * basisc.Z;
            e.Z = w.X * lookDir.X + w.Y * lookDir.Y + w.Z * lookDir.Z;
            
        }

        public bool ClipAgainstZ(CPoint3D_Node a1, CPoint3D_Node a2)
        {
            float t;

            if (    (a1.Z <= front && a2.Z <= front)
                ||  (a1.Z >= back && a2.Z >= back)
               )
            {
                return false;
            }



            if ((a1.Z < front && a2.Z > front) ||
                (a1.Z > front && a2.Z < front))
            {

                t = (front - a1.Z) / (a2.Z - a1.Z);
                if (a1.Z < front)
                {
                    a1.X = a1.X + t * (a2.X - a1.X);
                    a1.Y = a1.Y + t * (a2.Y - a1.Y);
                    a1.Z = front;
                }
                else
                {
                    a2.X = a1.X + t * (a2.X - a1.X);
                    a2.Y = a1.Y + t * (a2.Y - a1.Y);
                    a2.Z = front;
                }
            }

            if ((a1.Z < back && a2.Z > back) ||
                (a1.Z > back && a2.Z < back))
            {
                t = (back - a1.Z) / (a2.Z - a1.Z);
                if (a1.Z < back)
                {
                    a2.X = a1.X + t * (a2.X - a1.X);
                    a2.Y = a1.Y + t * (a2.Y - a1.Y);
                    a2.Z = back;
                }
                else
                {
                    a1.X = a1.X + t * (a2.X - a1.X);
                    a1.Y = a1.Y + t * (a2.Y - a1.Y);
                    a1.Z = back;
                }
            }

            return true;
        }

        public bool ClipAgainst_X_and_Y(CPoint3D_Node p1, CPoint3D_Node p2)
        {
            float t;

            if (    (p1.X >= 1 && p2.X >= 1)
                ||  (p1.X <= -1 && p2.X <= -1)
                )
            {
                return false;
            }


            if ((p1.X > 1 && p2.X < 1) || (p1.X < 1 && p2.X > 1))
            {
                t = (1 - p1.X) / (p2.X - p1.X);
                if (p1.X < 1)
                {
                    p2.Y = p1.Y + t * (p2.Y - p1.Y);
                    p2.X = 1;
                }
                else
                {
                    p1.Y = p1.Y + t * (p2.Y - p1.Y);
                    p1.X = 1;
                }
            }


            if ((p1.X < -1 && p2.X > -1) || (p1.X > -1 && p2.X < -1))
            {

                t = (-1 - p1.X) / (p2.X - p1.X);
                if (p1.X > -1)
                {
                    p2.Y = p1.Y + t * (p2.Y - p1.Y);
                    p2.X = -1;
                }
                else
                {
                    p1.Y = p1.Y + t * (p2.Y - p1.Y);
                    p1.X = -1;
                }
            }

            if (    (p1.Y >= 1 && p2.Y >= 1)
                ||  (p1.Y <= -1 && p2.Y <= -1))
                return false;

            if ((p1.Y > 1 && p2.Y < 1) || (p1.Y < 1 && p2.Y > 1))
            {

                t = (1 - p1.Y) / (p2.Y - p1.Y);
                if (p1.Y < 1)
                {
                    p2.X = p1.X + t * (p2.X - p1.X);
                    p2.Y = 1;
                }
                else
                {
                    p1.X = p1.X + t * (p2.X - p1.X);
                    p1.Y = 1;
                }
            }

            if ((p1.Y < -1 && p2.Y > -1) || (p1.Y > -1 && p2.Y < -1))
            {
                t = (-1 - p1.Y) / (p2.Y - p1.Y);
                if (p1.Y > -1)
                {
                    p2.X = p1.X + t * (p2.X - p1.X);
                    p2.Y = -1;
                }
                else
                {
                    p1.X = p1.X + t * (p2.X - p1.X);
                    p1.Y = -1;
                }
            }

            return (true);
        }

        public void NormalizeFov(CPoint3D_Node p)
        {
            p.X /= tanH;
            p.Y /= tanV;
        }

        public void ViewMapping(CPoint3D_Node p)
        {
            p.X = (int)(ceneterX + cxScreen * p.X / 2);
            p.Y = (int)(ceneterY - cyScreen * p.Y / 2);
        }

        public bool TransformToOrigin_And_Rotate_And_Project(CPoint3D_Node w1, CPoint3D_Node w2)
        {
            CPoint3D_Node e1 = new CPoint3D_Node(0, 0, 0);            
            TransformToOrigin_And_Rotate(w1, e1);

            CPoint3D_Node e2 = new CPoint3D_Node(0, 0, 0);            
            TransformToOrigin_And_Rotate(w2, e2);

            if (!ClipAgainstZ(e1, e2))
                return false;

            CPoint3D_Node p1, p2;
            p1 = new CPoint3D_Node(0, 0, 0);
            p2 = new CPoint3D_Node(0, 0, 0);
            Projection.DoPrespectiveProjection(e1, p1, focal);
            Projection.DoPrespectiveProjection(e2, p2, focal);


            NormalizeFov(p1);
            NormalizeFov(p2);

            if (!ClipAgainst_X_and_Y(p1, p2))
                return false;

            ViewMapping(p1);
            ViewMapping(p2);

            w1.X = p1.X;
            w1.Y = p1.Y;

            w2.X = p2.X;
            w2.Y = p2.Y;
            return true;
        }
    }
}
