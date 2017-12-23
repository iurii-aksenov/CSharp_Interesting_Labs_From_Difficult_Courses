using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task1
{
    /// <summary>
    /// Цех присвоения Id продукту
    /// </summary>
    public class AssemblyShopId : BaseAssemblyShop
    {
        private static int _globalId = 0;
        private object _sync = new object();

        public AssemblyShopId(int assemblyShopId, SyncQueue<Product> inputLine, SyncQueue<Product> outputLine, CancellationToken cancellationToken) : base(assemblyShopId, inputLine, outputLine, cancellationToken) { }

        public override void Make(Product product)
        {
            lock (_sync)
            {
                _globalId++;
                product.Id = _globalId;
            }
        }
    }

    /// <summary>
    /// Цех присвоения Формы продукту
    /// </summary>
    public class AssemblyShopShape : BaseAssemblyShop
    {
        public AssemblyShopShape(int assemblyShopId, SyncQueue<Product> inputLine, SyncQueue<Product> outputLine, CancellationToken cancellationToken) : base(assemblyShopId, inputLine, outputLine, cancellationToken) { }

        public override void Make(Product product)
        {
            product.Shape = (Shape)(Random.Next((int)Shape.Triangle) + 1);
        }

    }

    /// <summary>
    /// Цех присвоения Цвета продукту
    /// </summary>
    public class AssemblyShopColor : BaseAssemblyShop
    {
        public AssemblyShopColor(int assemblyShopId, SyncQueue<Product> inputLine, SyncQueue<Product> outputLine, CancellationToken cancellationToken) : base(assemblyShopId, inputLine, outputLine, cancellationToken) { }

        public override void Make(Product product)
        {
            product.Color = (Color)(Random.Next((int)Color.Black) + 1);
        }
    }

    /// <summary>
    /// Цех присвоения Маркировки продукту
    /// </summary>
    public class AssemblyShopLabel : BaseAssemblyShop
    {
        public AssemblyShopLabel(int assemblyShopId, SyncQueue<Product> inputLine, SyncQueue<Product> outputLine, CancellationToken cancellationToken) : base(assemblyShopId, inputLine, outputLine, cancellationToken) { }

        public override void Make(Product product)
        {
            product.Label = (Label)(Random.Next((int)Label.L0111) + 1); ;
        }
    }

    /// <summary>
    /// Цех присвоения Упаковки продукту
    /// </summary>
    public class AssemblyShopPack : BaseAssemblyShop
    {
        public AssemblyShopPack(int assemblyShopId, SyncQueue<Product> inputLine, SyncQueue<Product> outputLine, CancellationToken cancellationToken) : base(assemblyShopId, inputLine, outputLine, cancellationToken) { }

        public override void Make(Product product)
        {
            product.Pack = (Pack)(Random.Next((int)Pack.Membrane) + 1);
        }
    }
}
