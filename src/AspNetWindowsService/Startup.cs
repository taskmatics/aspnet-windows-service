using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MyDnxService
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder appBuilder)
        {
            var applicationLifetime = appBuilder.ApplicationServices.GetRequiredService<IApplicationLifetime>();
            applicationLifetime.ApplicationStarted.Register(() => Console.WriteLine("Application started."));
            applicationLifetime.ApplicationStopped.Register(() => Console.WriteLine("Application stopped."));

            appBuilder.UseDefaultFiles();
            appBuilder.UseStaticFiles();
            appBuilder.UseMvc();
        }
    }
}
