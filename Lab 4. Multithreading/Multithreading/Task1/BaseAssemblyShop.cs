using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task1
{
    public abstract class BaseAssemblyShop
    {
        public Random Random { get; } =  new Random();

        private readonly int _assemblyShopId;

        private readonly Thread[] _threads = new Thread[4];
        private readonly SyncQueue<Product> _input;
        private readonly SyncQueue<Product> _output;

        private readonly CancellationToken _cancelToken;

        protected BaseAssemblyShop(int assemblyShopId, SyncQueue<Product> inputLine, SyncQueue<Product> outputLine, CancellationToken cancelToken)
        {
            _assemblyShopId = assemblyShopId;
            _input = inputLine;
            _output = outputLine;
            _cancelToken = cancelToken;
        }

        public void Start()
        {
            for (int i = 0; i < 4; i++)
            {
                _threads[i] = new Thread(AssemblyProduct);
                _threads[i].Start(_cancelToken);
            }
        }

        //public void Stop()
        //{
        //    //_tokenSource.Cancel();
        //}

        public abstract void Make(Product product);

        private void AssemblyProduct(object cancelToken)
        {
            while (true)
            {
                //int threadIndex = (int)threadContext;
                //Console.WriteLine("thread Pack {0} started...", threadIndex);

                if (_cancelToken.IsCancellationRequested)
                {
                    return;
                }

                HardWork();// имитация сложной работы
                var product = _input.Dequeue();

                Make(product);

                PrintInformationAboutAssemblyShop(product.ToProductInfo());
                //Console.WriteLine("thread Pack {0} result calculated productId {1}...", threadIndex, product.Id);
                _output.Enqueue(product);
            }
        }

        private void HardWork()
        {
            int delay = 0;
            delay = Random.Next(3000) + 1;
            Thread.Sleep(delay);
        }

        private void PrintInformationAboutAssemblyShop(ProductInfo product)
        {
            Console.WriteLine($"AssemblyShop {_assemblyShopId}. {product}");
        }
    }
}


