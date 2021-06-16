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

namespace _01_Configuration
{
    public class Startup
    {
        public IConfiguration AppConfiguration { get; set; }

        public Startup()
        {
            var builder = new ConfigurationBuilder();
            builder.AddInMemoryCollection(new Dictionary<string, string>
            {
                {"Name", "VovA"},
                {"Surname", "KachaN"}
            });
            AppConfiguration = builder.Build();

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            string name = $"<p><br>Name: {AppConfiguration["Name"]}</br></p>";
            string surname = $"<p><br>Surname: {AppConfiguration["Surname"]}</br></p>";

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync(name + surname);
                });
            });
        }
    }
}
