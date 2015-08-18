using System;
using SourceAFIS;
using SourceAFIS.Test;
using Test.BrokerService;

namespace Test
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var image = TestUtils.LoadImage(@"D:\ScarIntelFinal\ScarIntel\BD\filipe_dir_an\filipe_dir_an_2.bmp");
            var fp = new FingerprintTemplate(image);
            
            Console.WriteLine(fp);

            /*
            var server = new ServerServiceClient();
            var p = new Person();
            p.Address = "";
            p.Birthday = DateTime.Now;
            p.Birthplace = "Cv creb txeu";
            p.Name = "Rafael Delgado";
            int id = server.InsertPerson(p);
            Console.WriteLine(" Insert id = {0}", id);*/
        }
    }
}