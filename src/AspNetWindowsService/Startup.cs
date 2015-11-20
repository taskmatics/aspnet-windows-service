using Microsoft.AspNet.Builder;
using Microsoft.Extensions.DependencyInjection;

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
            appBuilder.UseDefaultFiles();
            appBuilder.UseStaticFiles();
            appBuilder.UseMvc();
        }
    }
}
