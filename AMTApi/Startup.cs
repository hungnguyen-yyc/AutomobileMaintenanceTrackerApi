using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AMTDll;
using AMTDll.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AMTApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Should be more restricted
            // Use this only for sake of local debugging
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddTransient<IRepository<VehicleModel>, Repository<VehicleModel>>();
            services.AddTransient<IRepository<ServiceModel>, Repository<ServiceModel>>();
            services.AddTransient<IRepository<ServiceProviderModel>, Repository<ServiceProviderModel>>();
            services.AddTransient<IServicesValidation, ServicesValidation>();

            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("MyPolicy");

            app.UseMvc();
        }
    }
}
