using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task1
{
    public class SyncQueue<T>
    {
        readonly Queue<T> _queue = new Queue<T>();

        private readonly SemaphoreSlim _capacity = new SemaphoreSlim(4, 4);
        private readonly SemaphoreSlim _size = new SemaphoreSlim(0, 4);
        private readonly object _sync = new object();

        public void Enqueue(T item)
        {

            _capacity.Wait();
            lock (_sync)
            {
                _queue.Enqueue(item);
                _size.Release();
            }
        }

        public T Dequeue()
        {
            _size.Wait();
            lock (_sync)
            {
                var result = _queue.Dequeue();
                _capacity.Release();
                return result;
            }
        }

    }
}
