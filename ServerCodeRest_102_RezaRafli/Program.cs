using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using ServiceRest_102_RezaRafli;

namespace ServerCodeRest_102_RezaRafli
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost hostObj = null;
            Uri address = new Uri("http://localhost:1907/Mahasiswa");
            WebHttpBinding bind = new WebHttpBinding();
            try
            {
                hostObj = new ServiceHost(typeof(TI_UMY), address);
                //ALAMAT BASE ADDRESS
                hostObj.AddServiceEndpoint(typeof(ITI_UMY), bind, "");
                //ALAMAT ENDPOINT
                //wsdl
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior(); //Service Runtime Player
                smb.HttpGetEnabled = true; //untuk mengaktifkan wsdl (dibuka saat development, tidak untuk dibuka)
                hostObj.Description.Behaviors.Add(smb);
                //mex
                Binding mexbind = MetadataExchangeBindings.CreateMexHttpBinding();
                hostObj.AddServiceEndpoint(typeof(IMetadataExchange),mexbind, "mex");

                WebHttpBehavior whb = new WebHttpBehavior();
                whb.HelpEnabled = true;
                hostObj.Description.Endpoints[0].EndpointBehaviors.Add(whb);
                
                hostObj.Open();
                Console.WriteLine("Server is ready!!!!");
                Console.ReadLine();
                hostObj.Close();
            }
            catch (Exception ex)
            {
                hostObj = null;
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
    }
}
