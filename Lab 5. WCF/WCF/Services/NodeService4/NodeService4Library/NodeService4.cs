using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace NodeService4Library
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде и файле конфигурации.
    public class NodeService4 : INodeService4
    {
        public double MagicOperation(double x, double y)
        {
            return (x + y) / (x - y);
        }

        public double SumOfSquares(double x, double y)
        {
            return x * x + y * y;
        }
    }
}
