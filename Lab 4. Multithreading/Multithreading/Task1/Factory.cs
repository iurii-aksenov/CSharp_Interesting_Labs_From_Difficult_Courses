using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task1
{
    public class Factory
    {

        private readonly SyncQueue<Product> _zero = new SyncQueue<Product>();
        private readonly SyncQueue<Product> _afterMakingId = new SyncQueue<Product>();
        private readonly SyncQueue<Product> _afterMakingShape = new SyncQueue<Product>();
        private readonly SyncQueue<Product> _afterMakingColor = new SyncQueue<Product>();
        private readonly SyncQueue<Product> _afterMakingLabel = new SyncQueue<Product>();
        private readonly SyncQueue<Product> _afterMakingPack = new SyncQueue<Product>();

        private readonly BaseAssemblyShop[] _assemblyShops;

        private readonly int _productsCount;
        private readonly List<Product> _products;

        private readonly Thread _threadLoad;
        private readonly Thread _threadUpload;

        readonly CancellationTokenSource _cancelSource = new CancellationTokenSource();
        private CancellationToken _cancelToken;

        public Factory() : this(10) { }
        public Factory(int amountOfTheProducts)
        {
            _cancelToken = _cancelSource.Token;
            _productsCount = amountOfTheProducts;
            _assemblyShops = new BaseAssemblyShop[5];
            _assemblyShops[0] = new AssemblyShopId(1, _zero, _afterMakingId, _cancelToken);
            _assemblyShops[1] = new AssemblyShopShape(2, _afterMakingId, _afterMakingShape, _cancelToken);
            _assemblyShops[2] = new AssemblyShopColor(3, _afterMakingShape, _afterMakingColor, _cancelToken);
            _assemblyShops[3] = new AssemblyShopLabel(4, _afterMakingColor, _afterMakingLabel, _cancelToken);
            _assemblyShops[4] = new AssemblyShopPack(5, _afterMakingLabel, _afterMakingPack, _cancelToken);

            _products = new List<Product>(_productsCount);

            _threadLoad = new Thread(LoadMaterial);
            _threadUpload = new Thread(UploadMaterial);

            _cancelToken = _cancelSource.Token;
        }

        public IReadOnlyList<Product> GetProducts()
        {
            return _products;
        }

        public void StartFactory()
        {
            try
            {
                _threadLoad.Start();

                foreach (var assemblyShop in _assemblyShops)
                {
                    assemblyShop.Start();

                }

                _threadUpload.Start();

            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка в старте фабрики");
                Console.WriteLine(e);
            }
        }

        public void StopFactory()
        {
            try
            {
                _cancelSource.Cancel();

                //foreach (var assemblyShop in _assemblyShops)
                //{
                //    assemblyShop.Stop();
                //}
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка в остановке фабрики");
                Console.WriteLine(e);
            }
        }

        private void LoadMaterial()
        {
            for (int i = 0; i < _productsCount; i++)
            {
                if (_cancelToken.IsCancellationRequested)
                {
                    return;
                }
                _zero.Enqueue(new Product());
            }
        }

        private void UploadMaterial()
        {

            for (int i = 0; i < _productsCount; i++)
            {
                if (_cancelToken.IsCancellationRequested)
                {
                    return;
                }
                _products.Add(_afterMakingPack.Dequeue());
            }
        }
    }
}
