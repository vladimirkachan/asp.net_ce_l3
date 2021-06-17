using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace _04_JsonSectionAndChildren
{
    public class Startup
    {
        public IConfiguration Configuration {get; set;}

        public Startup(IWebHostEnvironment environment)
        {
            ConfigurationBuilder builder = new();
            builder.SetBasePath(environment.ContentRootPath);
            builder.AddJsonFile("Data.json");
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            IConfigurationSection section1 = Configuration.GetSection("Company"),
                                  section2 = Configuration.GetSection("Phones");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync($"<h2>Company: {section1.GetSection("Name").Value}</h2>");
                    await context.Response.WriteAsync($"<h2>Country: {section1.GetSection("Country").Value}</h2><hr />");
                    foreach (var phone in section2.GetChildren())
                        await context.Response.WriteAsync($"<p>{phone.Key} = ${phone.GetSection("Price").Value}</p>");
                });
            });
        }
    }
}
