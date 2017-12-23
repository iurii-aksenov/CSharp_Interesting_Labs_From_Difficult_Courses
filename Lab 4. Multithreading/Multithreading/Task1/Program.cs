using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            Factory factory = new Factory(500);
            factory.StartFactory();

            Console.WriteLine("Enter \"Space\" Or \"Enter\" to stop the factory");
            var key = Console.ReadKey();
            if (key.Key== ConsoleKey.Spacebar || key.Key == ConsoleKey.Enter)
            {

                Console.WriteLine("Stopping ...");
                factory.StopFactory();
                
            }

        

            //waitAllProducts.Join();

            //Console.WriteLine("-------------------------------------------------------------\n");

            //foreach (var product in _products)
            //{
            //    Console.WriteLine(product.ToString());
            //}
            //Console.WriteLine("-------------------------------------------------------------\n");
            //Console.WriteLine("Ообработка всех продуктов завершена..");

            //Console.ReadLine();

        }
    }
}

