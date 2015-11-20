using Microsoft.AspNet.Hosting;
using System;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;

namespace MyDnxService
{
    public class Program : ServiceBase
    {
        private StopApplication _stopApplication;

        public static void Main(string[] args)
        {
            try
            {
                if (args.Contains("--windows-service"))
                {
                    Run(new Program());
                    Debug.WriteLine("Exiting");
                    return;
                }

                var program = new Program();
                program.OnStart(null);
                Console.ReadLine();
                program.OnStop();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                _stopApplication = WebApplication.Run(new[] {
                    "server.urls=http://localhost:5000",
                    "server=Microsoft.AspNet.Server.Kestrel"
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine("error in OnStart: " + ex);
                throw;
            }
        }

        protected override void OnStop()
        {
            _stopApplication?.Invoke();
        }
    }
}
