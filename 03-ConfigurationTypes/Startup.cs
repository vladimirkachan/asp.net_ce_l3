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

namespace _03_ConfigurationTypes
{
    public class Startup
    {
        public IConfiguration Configuration {get; set;}

        public Startup(IWebHostEnvironment environment)
        {
            ConfigurationBuilder builder = new();
            builder.SetBasePath(environment.ContentRootPath);
            builder.AddIniFile("IniConfig.ini");
            builder.AddJsonFile("JsonConfig.json");
            builder.AddXmlFile("XmlConfig.xml");
            builder.AddInMemoryCollection(new Dictionary<string,string>
            {
                {"ColorMemory", "Blue"},
                {"ContentMemory","Configuration from Memory"}
            });
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
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                string OutputString(string color, string content)
                {
                    return $@"<p style='color:{color}; font-size:24px'>{content}</p>";
                }

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync(OutputString(Configuration["ColorMemory"], Configuration["ContentMemory"]));
                    await context.Response.WriteAsync(OutputString(Configuration["ColorIni"], Configuration["ContentIni"]));
                    await context.Response.WriteAsync(OutputString(Configuration["ColorJson"], Configuration["ContentJson"]));
                    await context.Response.WriteAsync(OutputString(Configuration["ColorXml"], Configuration["ContentXml"]));
                });
            });
        }
    }
}
