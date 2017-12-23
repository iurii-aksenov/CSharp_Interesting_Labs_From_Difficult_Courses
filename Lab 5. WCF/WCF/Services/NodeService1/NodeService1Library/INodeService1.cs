using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace NodeService1Library
{
    [ServiceContract]
    public interface INodeService1
    {
        [OperationContract]
        double Add(double x, double y);
        [OperationContract]
        double Substract(double x, double y);
    }

}
