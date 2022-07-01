using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace _08_Step
{
    class Parmetric_Model : _3D_Model
    {
        public int Rad = 30;
        public int N = 10;

        public void Design()
        {
            float x = 0, y = 0;
            float Z = 0;
            int iP = 0;
            int iS = 0;
            int ctP = 0;
            float vX = -230;
            float vY = 0;
            float savedVX = 0;

            for (int k = 0; k < 2; k++)
            {
                for (int i = 0; i < 200; i++)
                {
                    ctP++;
                    x = vX + i;
                    y = vY;

                    CPoint3D_Node pnn = new CPoint3D_Node(x, y, Z);
                    AddPoint(pnn);

                    if (iP > 0 && i > 0)
                        AddEdge(iP, iP - 1, Color.Red);

                    if (k == 1 && i % 20 == 0)
                        AddEdge(iP, iP - 200, Color.Red);

                    iP++;
                }

                Z += 50;
            }

            Z = 0;

            for (int k = 0; k < 2; k++)
            {
                for (int i = 0; i < N; i++)
                {
                    x = (float)(Rad * Math.Cos(i * Math.PI / 9));
                    y = (float)(Rad * Math.Sin(i * Math.PI / 9));

                    CPoint3D_Node pnn = new CPoint3D_Node(x, y, Z);
                    AddPoint(pnn);

                    if (iP > 0 && i > 0)
                        AddEdge(iP, iP - 1, Color.Black);

                    if (iP >= N + ctP && i > 0)
                        AddEdge(iP, iP - N, Color.Black);

                    iP++;
                }

                iS += N;
                Z += 50;
            }

            vX = (float)(Rad * Math.Cos(0 * Math.PI / N));
            vY = (float)(Rad * Math.Sin(0 * Math.PI / N));
            ctP += N * 2;
            Z = 0;

            for (int k = 0; k < 2; k++)
            {
                for (int i = 0; i < 100; i++)
                {
                    ctP++;
                    x = vX + i;
                    y = vY;

                    CPoint3D_Node pnn = new CPoint3D_Node(x, y, Z);
                    AddPoint(pnn);

                    if (iP > 0 && i > 0)
                        AddEdge(iP, iP - 1, Color.Yellow);

                    if(k == 1 && i % 20 == 0)
                        AddEdge(iP, iP - 100, Color.Yellow);

                    iP++;
                }

                Z += 50;
            }

            vX = x + 20;
            Z = 0;
            iS = 0;

            for (int k = 0; k < 2; k++)
            {
                for (int i = 0; i < N; i++)
                {
                    x = vX + i + (float)(Rad * Math.Cos(i * Math.PI / 9));
                    y = (float)(Rad * Math.Sin(i * Math.PI / 9));

                    CPoint3D_Node pnn = new CPoint3D_Node(x, y, Z);
                    AddPoint(pnn);

                    if (iP > 0 && i > 0)
                        AddEdge(iP, iP - 1, Color.Orange);// 1 to 0, 2 to 1

                    if (iP >= N + ctP && i > 0)
                        AddEdge(iP, iP - N, Color.Orange);//

                    iP++;
                }

                iS += N;
                Z += 50;
            }

            vX += 30;
            ctP += N * 2;
            Z = 0;

            for (int k = 0; k < 2; k++)
            {
                for (int i = 0; i < 100; i++)
                {
                    ctP++;
                    x = vX + i;
                    y = vY;

                    CPoint3D_Node pnn = new CPoint3D_Node(x, y, Z);
                    AddPoint(pnn);

                    if (iP > 0 && i > 0)
                        AddEdge(iP, iP - 1, Color.Blue);

                    if (k == 1 && i % 20 == 0)
                        AddEdge(iP, iP - 100, Color.Blue);

                    iP++;
                }

                Z += 50;
            }

            Z = 0;
            savedVX = vX;
            vX = -230;
            vY += 100;

            for (int k = 0; k < 2; k++)
            {
                if(k == 0)
                    AddEdge(iP, 0, Color.White);
                else
                    AddEdge(iP, 200, Color.White);

                for (int i = 0; i < 408; i++)
                {
                    ctP++;
                    x = vX + i;
                    y = vY;

                    CPoint3D_Node pnn = new CPoint3D_Node(x, y, Z);
                    AddPoint(pnn);

                    if (iP > 0 && i > 0)
                        AddEdge(iP, iP - 1, Color.Pink);

                    if (k == 1 && i % 20 == 0 && (i < 300 || i > 340) && i < 390)
                        AddEdge(iP, iP - 408, Color.Pink);

                    iP++;
                }

                Z += 50;
            }

            Z = 0;
            Rad = 100;
            vX = savedVX;

            for (int k = 0; k < 2; k++)
            {
                if (k == 1)
                    AddEdge(iP, iP - N, Color.Green);

                for (int i = 0; i < N; i++)
                {
                    x = vX + i + (float)(Rad * Math.Cos(i * Math.PI / 17));
                    y = (float)(Rad * Math.Sin(i * Math.PI / 17));

                    CPoint3D_Node pnn = new CPoint3D_Node(x, y, Z);
                    AddPoint(pnn);

                    if (iP > 0 && i > 0)
                        AddEdge(iP, iP - 1, Color.Green);

                    if (iP >= N + ctP && i > 0)
                        AddEdge(iP, iP - N, Color.Green);

                    iP++;
                }

                iS += N;
                Z += 50;
            }

        }
    }
}
