using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task1
{
    class TestSyncListSimple
    {
        private static SyncQueue<int> ar = new SyncQueue<int>();
        public static void Start()
        {

            Thread w = new Thread(Write);
            Thread r = new Thread(Read);

            w.Start();
            r.Start();

            //Thread.Sleep(10000);
            // Console.WriteLine("Консоль ожидает..");
            Console.ReadLine();

        }

        public static void Write()
        {
            for (int i = 0 + 1; i < 8 + 1; i++)
            {
                Console.WriteLine("Producer writing... " + i);
                ar.Enqueue(i);
                Console.WriteLine("Producer write: " + i);
            }
        }

        public static void Read()
        {

            for (int i = 0; i < 8; i++)
            {
                Console.WriteLine("Producer reading...");
                int result = ar.Dequeue();
                Console.WriteLine("Consumer read: " + result);
            }
        }

        public void Print()
        {

        }
    }
}
