using AsyncDemo.WebApi.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;

namespace AsyncDemo.WebApi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSingleton<SlowApi>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                Interlocked.Increment(ref Program.Requests);
                await next();
                Interlocked.Decrement(ref Program.Requests);
            });

            app.UseMvc();
        }
    }
}
