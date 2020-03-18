using Detego.RfidReader.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Detego.RfidReader.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<Rfid.RfidReader>();
            services.AddSingleton<IRfidReader, RfidReaderWrapper>();
            services.AddSingleton<Tags>();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //initiates subscription
            app.ApplicationServices.GetRequiredService<Tags>();
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}