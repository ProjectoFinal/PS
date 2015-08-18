using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace Broker
{
    internal class Program
    {
        public static void Main()
        {
            //using (ServiceHost host = new ServiceHost(typeof(BrokerServer)))
            using (var host = new ServiceHost(new BrokerServer()))
            {
                // Open the ServiceHost to start listening for messages. Since
                // no endpoints are explicitly configured, the runtime will create
                // one endpoint per base address for each service contract implemented
                // by the service.

                host.Open();

                DumpEndpoint(host.Description.Endpoints);
                Console.WriteLine("\nPress <Enter> to stop the service.");
                while (true)
                {
                    String command = Console.ReadLine();

                    if (command.ToLower().Equals(""))
                    {
                        break;
                    }

                    if (command.ToLower().Equals("users"))
                    {
                        var server = (BrokerServer) host.SingletonInstance;
                        if (server != null)
                        {
                            server.PrintCurrentUsers();
                        }
                        else
                        {
                            Console.WriteLine("Sever not found ");
                        }
                    }
                }

                host.Close();
            }
        }

        private static void DumpEndpoint(ServiceEndpointCollection endpoints)
        {
            foreach (ServiceEndpoint sep in endpoints)
            {
                Console.Write("Address:{0}\nBinding:{1}\nContract:{2}\n", sep.Address, sep.Binding.Name, sep.Contract);
                Console.WriteLine("Binding Stack:");

                foreach (BindingElement be in sep.Binding.CreateBindingElements())
                {
                    Console.WriteLine(be.ToString());
                }
            }
        }
    }
}