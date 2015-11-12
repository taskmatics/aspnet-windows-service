using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Hosting.Internal;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.Configuration.Memory;
using Microsoft.Framework.DependencyInjection;
using System;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;

namespace MyDnxService
{
    public class Program : ServiceBase
    {
        private readonly IServiceProvider _serviceProvider;
        private IHostingEngine _hostingEngine;
        private IDisposable _shutdownServerDisposable;

        public Program(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Main(string[] args)
        {
            try
            {
                if (args.Contains("--windows-service"))
                {
                    Run(this);
                    Debug.WriteLine("Exiting");
                    return;
                }

                OnStart(null);
                Console.ReadLine();
                OnStop();
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
                Microsoft.Dnx.Runtime.Infrastructure.CallContextServiceLocator.Locator.ServiceProvider = _serviceProvider;

                var configProvider = new MemoryConfigurationProvider();
                configProvider.Add("server.urls", "http://localhost:5000");

                var config = new ConfigurationBuilder(configProvider).Build();
                var builder = new WebHostBuilder(_serviceProvider, config);
                builder.UseServer("Microsoft.AspNet.Server.Kestrel");
                builder.UseServices(services => services.AddMvc());
                builder.UseStartup(appBuilder =>
                {
                    appBuilder.UseDefaultFiles();
                    appBuilder.UseStaticFiles();
                    appBuilder.UseMvc();
                });

                _hostingEngine = builder.Build();
                _shutdownServerDisposable = _hostingEngine.Start();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("error in OnStart: " + ex);
                throw;
            }
        }

        protected override void OnStop()
        {
            if (_shutdownServerDisposable != null)
                _shutdownServerDisposable.Dispose();
        }
    }
}
