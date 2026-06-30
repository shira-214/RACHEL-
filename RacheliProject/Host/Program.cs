    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Server;

namespace Host
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                SeedData.SeedIfEmpty();

                using (ServiceHost myservice = new ServiceHost(typeof(Server.Service1)))
                {
                    myservice.Open();
                    Console.WriteLine("Service is running...");
                    Console.WriteLine("Press Enter to stop the service.");
                    Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
