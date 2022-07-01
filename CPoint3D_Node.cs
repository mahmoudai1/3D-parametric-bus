using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _08_Step
{
    class CPoint3D_Node
    {
        public float X, Y, Z;

        public CPoint3D_Node( CPoint3D_Node S)
        {
            X = S.X;
            Y = S.Y;
            Z = S.Z;
        }

        public CPoint3D_Node(float a, float b, float c)
        {
            X = a;
            Y = b;
            Z = c;
        }
    }
}
