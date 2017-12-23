using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace NodeService4Library
{
    [ServiceContract]
    public interface INodeService4
    {
        [OperationContract]
        double SumOfSquares(double x, double y);
        [OperationContract]
        double MagicOperation(double x, double y);
    }
}
