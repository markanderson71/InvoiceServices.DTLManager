using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using InvoiceServices.DTLManager.Core;
using InvoiceServices.DTLManager.DB;
using AutoMapper;

namespace InvoiceServices.DTLManager
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
            services.AddMvc();

            var config = new AutoMapper.MapperConfiguration(cfg => { cfg.AddProfile(new AutoMapperConfigurationProfile()); });
            var mapper = config.CreateMapper();
            services.AddSingleton<IMapper>(mapper);


            services.Configure<DatabaseSettings>(c =>
            { 
                c.ConnectionString = Configuration.GetSection("MongoConnection:ConnectionString").Value;
                c.DatabaseName = Configuration.GetSection("MongoConnection:Database").Value;
            });
            services.AddScoped(cfg => cfg.GetService<IOptions<DatabaseSettings>>().Value);
            
            services.AddScoped<IRepository,MongoDb>();
          

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
