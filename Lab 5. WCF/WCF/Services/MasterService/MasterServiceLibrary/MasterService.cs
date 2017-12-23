using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MasterServiceLibrary.NodeService1;
using MasterServiceLibrary.NodeService2;
using MasterServiceLibrary.NodeService3;
using MasterServiceLibrary.NodeService4;

namespace MasterServiceLibrary
{
    

    public class MasterService : IMasterService
    {
       private readonly  NodeService1Client _service1;
        private readonly NodeService2Client _service2;
        private readonly NodeService3Client _service3;
        private readonly NodeService4Client _service4;
        MasterService()
        {
            _service1 = new NodeService1Client("NetTcpBinding_INodeService1");
            _service2 = new NodeService2Client("NetTcpBinding_INodeService2");
            _service3 = new NodeService3Client("NetTcpBinding_INodeService3");
            _service4 = new NodeService4Client("NetTcpBinding_INodeService4");
        }
       

        public double Add(double x, double y)
        {
            return _service1.Add(x, y);
        }

        public double Substract(double x, double y)
        {
            
            return _service1.Substract(x, y);
        }

        public double Multiply(double x, double y)
        {
            return _service2.Multiply(x, y);
        }

        public double Divide(double x, double y)
        {
            return _service2.Divide(x, y);
        }

        public double Modulo(double x, double y)
        {
            return _service3.Modulo(x, y);
        }

        public double Pow(double x, double y)
        {
            return _service3.Pow(x, y);
        }

        public double SumOfSquares(double x, double y)
        {
            return _service4.SumOfSquares(x, y);
        }

        public double MagicOperation(double x, double y)
        {
            return _service4.MagicOperation(x, y);
        }
    }
}
