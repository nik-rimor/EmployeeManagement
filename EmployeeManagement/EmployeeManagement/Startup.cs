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
using Microsoft.Extensions.Logging;

namespace EmployeeManagement
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
                              ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.Use(async (context, next) =>
            {
                logger.LogInformation("M1: request received");
                await context.Response.WriteAsync("Hello from 1st Middleware!");
                await next();
                logger.LogInformation("M1: response produced");
            });

            app.Use(async (context, next) =>
            {
                logger.LogInformation("M2: request received");
                await context.Response.WriteAsync("Hello from 2nd Middleware!");
                await next();
                logger.LogInformation("M2: response produced");
            });

            app.Run(async context =>
            {
                logger.LogInformation("Request reqceiver from Terminal middleware");
                await context.Response.WriteAsync("Hello from 3rd Middleware!");
                logger.LogInformation("Response generated from Terminal midleware");
            });

            //app.UseEndpoints(endpoints =>
            //{
            //endpoints.MapGet("/", async context =>
            //{
            //    logger.LogInformation("MW1: Incoming Rerquest");
            //    await context.Response
            //        .WriteAsync(System.Diagnostics.Process.GetCurrentProcess().ProcessName +
            //                    "\n" + "MyKey : " + _config["MyKey"]);
            //    logger.LogInformation("MW1: Outgoing Response");
            //});

            //});
        }
    }
}
