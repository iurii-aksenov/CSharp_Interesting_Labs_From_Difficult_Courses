using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace NodeService3Library
{

    public class NodeService3 : INodeService3
    {
        public double Modulo(double x, double y)
        {
            return x % y;
        }

        public double Pow(double x, double y)
        {
            return Math.Pow(x, y);
        }
    }
}
