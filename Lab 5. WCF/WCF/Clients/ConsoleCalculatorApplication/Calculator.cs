using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalculatorApplication
{
    public class Calculator
    {
        private readonly CalculatorService.MasterServiceClient _client = new CalculatorService.MasterServiceClient();

        public double Add(double x, double y)
        {
            return _client.Add(x, y);
        }

        public double Substract(double x, double y)
        {
            return _client.Substract(x, y);
        }

        public double Multiply(double x, double y)
        {
            return _client.Multiply(x, y);
        }

        public double Divide(double x, double y)
        {
            return _client.Divide(x, y);
        }

        public double Modulo(double x, double y)
        {
            return _client.Modulo(x, y);
        }

        public double Pow(double x, double y)
        {
            return _client.Pow(x, y);
        }

        public double SumOfSquares(double x, double y)
        {
            return _client.SumOfSquares(x, y);
        }

        public double MagicOperation(double x, double y)
        {
            return _client.MagicOperation(x, y);
        }
    }
}
