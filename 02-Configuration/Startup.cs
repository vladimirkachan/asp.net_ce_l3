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

namespace _02_Configuration
{
    public class Startup
    {
        public IConfiguration AppConfiguration {get; set;}

        public Startup()
        {
            ConfigurationBuilder builder = new();
            builder.AddInMemoryCollection(new Dictionary<string, string>
            {
                {"ApplicationName", null},
                {"EnvironmentName", null}
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
            AppConfiguration["ApplicationName"] = env.ApplicationName;
            AppConfiguration["EnvironmentName"] = env.ContentRootPath;

            string body = $"<h1>Application name: {AppConfiguration["ApplicationName"]}</h1><br />";
            body += $"<h3>Environment name: {AppConfiguration["EnvironmentName"]}</h3>";

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync(body);
                });
            });
        }
    }
}
