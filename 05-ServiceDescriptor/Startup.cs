using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace _05_ServiceDescriptor
{
    public class Startup
    {
        IServiceCollection services;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            this.services = services;
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
                var body = new StringBuilder();
                body.Append("<h1 align=center>Services</h1>");
                body.Append("<table>");
                body.Append("<tr><th>ServiceType</th><th>Lifetime</th><th>ImplementationType</th></tr>");
                foreach (var sd in services)
                {
                    body.Append("<tr>");
                    body.Append($"<td>{sd.ServiceType.FullName}</td>");
                    body.Append($"<td>{sd.Lifetime}</td>");
                    body.Append($"<td>{sd.ImplementationType?.FullName}</td>");
                    body.Append("</tr>");
                }
                body.Append("</table>");

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync(body.ToString());
                });
            });
        }
    }
}
