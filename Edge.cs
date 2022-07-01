using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace _08_Step
{
    public class Edge
    {
        public int E0, E1;
        public Color cl;
        public Edge(int a, int b)
        {
            E0 = a;
            E1 = b;
        }
    }
}
