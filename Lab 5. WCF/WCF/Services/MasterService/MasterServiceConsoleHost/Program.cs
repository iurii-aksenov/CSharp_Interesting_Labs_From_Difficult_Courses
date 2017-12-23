using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MasterServiceConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Type serviceType = typeof(MasterServiceLibrary.MasterService);

                Console.WriteLine($"Service Name: {nameof(MasterServiceLibrary.MasterService)}");
                Console.WriteLine("--------------------------");

                using (ServiceHost host = new ServiceHost(serviceType))
                {
                    host.Open();
                    Console.WriteLine("Service is running...");
                    string cmd;
                    do
                    {
                        Console.Write('>');
                        cmd = Console.ReadLine();
                    } while (cmd != "exit");

                    host.Close();
                    Console.WriteLine("Service closed");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
