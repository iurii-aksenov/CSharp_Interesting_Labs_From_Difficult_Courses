using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalculatorApplication
{
    class Program
    {
        public static Calculator _calculator;

        static void Main(string[] args)
        {
            _calculator= new Calculator();

            Action<double, double> operation = null;

            int line = 0;
            while(line!=9)
            {
                Console.WriteLine("----------------------\n");
               Console.WriteLine("To Add numbers write 1");
                Console.WriteLine("To Substract numbers write 2");
                Console.WriteLine("To Multiply numbers write 3");
                Console.WriteLine("To Divide numbers write 4");
                Console.WriteLine("To Modulo numbers write 5");
                Console.WriteLine("To Pow numbers write 6");
                Console.WriteLine("To Get sum of squares number write 7");
                Console.WriteLine("To Get magic operation numbers write 8");
                Console.WriteLine("To Exit write 9");

                line = int.Parse(Console.ReadLine().Trim());

                switch (line)
                {
                    case 1:
                        operation = Add;
                        break;
                    case 2:
                        operation = Substract;
                        break;
                    case 3:
                        operation = Multiply;
                        break;
                    case 4:
                        operation = Divide;
                        break;
                    case 5:
                        operation = Modulo;
                        break;
                    case 6:
                        operation = Pow;
                        break;
                    case 7:
                        operation = SumOfSquares;
                        break;
                    case 8:
                        operation = MagicOperation;
                        break;
                    case 9:
                        Console.WriteLine("Вышли из кулькулятора");
                        return;
                    default:
                        Console.WriteLine("Неправильный ввод");
                        break;
                }

                if (operation != null)
                {
                    var leftOperand = ReadLeftOperand();
                    var rightOperand = ReadRightOperand();
                    operation.Invoke(leftOperand, rightOperand);
                    operation = null;
                }
            }
            

        }

        public static double ReadLeftOperand()
        {
            Console.WriteLine("Write X");
            return Double.Parse(Console.ReadLine().Trim());
        }

        public static double ReadRightOperand()
        {
            Console.WriteLine("Write Y");
            return Double.Parse(Console.ReadLine().Trim());
        }

        public static void Add(double x, double y)
        {
            var result = _calculator.Add(x, y);
            Console.WriteLine($"Result of Add is {result}");
        }

        public static void Substract(double x, double y)
        {
            var result = _calculator.Substract(x, y);
            Console.WriteLine($"Result of  Substract is {result}");
        }

        public static void Multiply(double x, double y)
        {
            var result = _calculator.Multiply(x, y);
            Console.WriteLine($"Result of Multiply is {result}");
        }

        public static void Divide(double x, double y)
        {
            var result = _calculator.Divide(x, y);
            Console.WriteLine($"Result of Divide is {result}");
        }

        public static void Modulo(double x, double y)
        {
            var result = _calculator.Modulo(x, y);
            Console.WriteLine($"Result of Modulo is {result}");
        }

        public static void Pow(double x, double y)
        {
            var result = _calculator.Pow(x, y);
            Console.WriteLine($"Result of Pow is {result}");
        }

        public static void SumOfSquares(double x, double y)
        {
            var result = _calculator.SumOfSquares(x, y);
            Console.WriteLine($"Result of SumOfSquares is {result}");
        }

        public static void MagicOperation(double x, double y)
        {
            var result = _calculator.MagicOperation(x, y);
            Console.WriteLine($"Result of MagicOperation is {result}");
        }
    }
}
