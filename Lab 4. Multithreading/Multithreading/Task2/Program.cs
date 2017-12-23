using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter N - number of companies");
            int n = int.Parse(Console.ReadLine());

            using (CommodityPrices commodityPrices = new CommodityPrices(n))
            {
                while (true)
                {
                    Console.WriteLine("Enter \"Space\" Or \"Enter\" to calculate average price.\n Enter other button to exit");
                    var key = Console.ReadKey();
                    if (key.Key == ConsoleKey.Spacebar || key.Key == ConsoleKey.Enter)
                    {
                        Console.WriteLine("Calculating ...");
                        Console.WriteLine(commodityPrices.GetAveragePrice());
                        Console.WriteLine("--------------------------------------------");
                    }
                }

            }
        }
    }

    public class CommodityPrices : IDisposable
    {
        struct PriceInfo
        {
            public int CountOfPrice { get; set; }
            public double SumOfPrice { get; set; }
        }

        static Random random = new Random();
        private List<string> fileNames;
        private CancellationTokenSource _cancelTokenSource;
        private CancellationToken _token;
        private string path = @"..\..\";
        private string name = "name";
        private string ext = ".txt";
        private int _numberOfCompanies;

        private Task[] _writerTasks;
        private Task<PriceInfo>[] _readerTasks;

        public CommodityPrices(int numberOfCompanies)
        {
            _numberOfCompanies = numberOfCompanies;
            _cancelTokenSource = new CancellationTokenSource();
            _token = _cancelTokenSource.Token;
            fileNames = new List<string>(numberOfCompanies + 1);
            _writerTasks = new Task[numberOfCompanies];
            _readerTasks = new Task<PriceInfo>[numberOfCompanies];

            for (int i = 0; i < _numberOfCompanies; i++)
            {
                string fileName = path + name + (i + 1) + ext;
                fileNames.Add(fileName);
                _writerTasks[i] = new Task(() => WritePrice(fileName, _token), TaskCreationOptions.LongRunning);
            }

            foreach (var task in _writerTasks)
            {
                task.Start();
            }
        }

        public double GetAveragePrice()
        {
            int countOfAllPrice = 1;
            double sumOfAllPrice = 0;

            for (int i = 0; i < _numberOfCompanies; i++)
            {
                string fileName = path + name + (i + 1) + ext;
                _readerTasks[i] = new Task<PriceInfo>(() => ReadPrice(fileName));
                _readerTasks[i].Start();
            }


            Task.WaitAll(_readerTasks); // ожидание всех потоков чтения, чтобы потом посчитать общее среднее

            foreach (var task in _readerTasks)
            {
                countOfAllPrice += task.Result.CountOfPrice;
                sumOfAllPrice += task.Result.SumOfPrice;
            }
            return sumOfAllPrice / countOfAllPrice;
        }

        private PriceInfo ReadPrice(string fileName)
        {
            int delay;
            delay = random.Next(500) + 1;
            Thread.Sleep(delay);

            int countOfPrice = 0;
            double sumOfPrice = 0;

            FileStream fs1 = new FileStream(fileNames.ToList<string>()[0], FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete);
            using (StreamReader rstream = new StreamReader(fs1))
            {
                string line;
                while (!string.IsNullOrEmpty(line = rstream.ReadLine()))
                {

                    countOfPrice++;
                    sumOfPrice += Double.Parse(line);
                }
            }
            return new PriceInfo { SumOfPrice = sumOfPrice, CountOfPrice = countOfPrice };
        }

        private void WritePrice(string filename, CancellationToken token)
        {
            FileStream fs1 = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite | FileShare.Delete | FileShare.Read | FileShare.Write, 1);
            using (StreamWriter fstream = new StreamWriter(fs1) { AutoFlush = true })
            {
                double price;
                int delay;
                while (true)
                {
                    if (token.IsCancellationRequested)
                    {
                        fstream.Dispose();
                        return;
                    }
                    price = 0.0 + random.NextDouble() + 1.0;
                    delay = random.Next(2000) + 1; // имитация задержки выставление новой цены
                    Thread.Sleep(delay);

                    fstream.WriteLine(price);

                }
            }

        }

        public void Dispose()
        {
            _cancelTokenSource?.Dispose();
        }
    }
}
