using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Hosting.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;

namespace MyDnxService
{
    public class Program : ServiceBase
    {
        private IApplication _application;

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
                var configProvider = new MemoryConfigurationProvider();
                configProvider.Add("server.urls", "http://localhost:5000");

                var config = new ConfigurationBuilder()
                    .Add(configProvider)
                    .Build();

                var builder = new WebHostBuilder(config);
                builder.UseServer("Microsoft.AspNet.Server.Kestrel");
                builder.UseServices(services => services.AddMvc());
                builder.UseStartup(appBuilder =>
                {
                    appBuilder.UseDefaultFiles();
                    appBuilder.UseStaticFiles();
                    appBuilder.UseMvc();
                });

                var hostingEngine = builder.Build();
                _application = hostingEngine.Start();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("error in OnStart: " + ex);
                throw;
            }
        }

        protected override void OnStop()
        {
            _application?.Dispose();
        }
    }
}
