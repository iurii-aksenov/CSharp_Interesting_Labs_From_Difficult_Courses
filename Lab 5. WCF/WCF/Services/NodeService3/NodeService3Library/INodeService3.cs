using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace NodeService3Library
{
    [ServiceContract]
    public interface INodeService3
    {
        [OperationContract]
        double Modulo(double x, double y);
        [OperationContract]
        double Pow(double x, double y);
    }
}
