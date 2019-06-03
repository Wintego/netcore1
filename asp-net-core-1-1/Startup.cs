using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Infrastructure.Implementations;
using WebStore.Infrastructure.Interfaces;
using WebStore.Infrastructure.Implementations;
using WebStore.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using WebStore.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebStore.Data;

namespace WebStore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<WebStoreContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<WebStoreContextInitializer>();

            services.AddSingleton<IEmployeesData, InMemoryEmployeesData>();
            services.AddScoped<IProductData, SqlProductData>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, WebStoreContextInitializer db)
        {
            db.InitializeAsync().Wait();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseMvc(route =>
            {
                route.MapRoute(
                    name: "default", 
                    template: "{Controller=Main}/{Action=Index}/{id?}");
            });
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
