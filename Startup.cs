using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebApplication1.DataModel;
using System.Diagnostics;
using System.Threading;

namespace WebApplication1
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(); 

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                //app.UseDeveloperExceptionPage();
                //app.UseExceptionHandler("/home/Error");
                //app.UseStatusCodePagesWithReExecute("/home/Error");
                app.UseMiddleware<ExceptionMiddleware>();
                app.UseMvc();                
            }
            else
            {

                //app.UseExceptionHandler(
                //      options =>
                //    {
                //        options.Run(
                //       async context =>
                //       {
                //           context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                //           context.Response.ContentType = "text/html";
                //           var ex = context.Features.Get<IExceptionHandlerFeature>();
                //           if (ex != null)
                //           {
                //               var err = $"<h1>Error: {ex.Error.Message}</h1>{ex.Error.StackTrace }";
                //               await context.Response.WriteAsync(err).ConfigureAwait(false);
                //           }
                //       });
                //    }
                //                   );
            }

            app.UseStaticFiles();
            /*
            app.UseStatusCodePages(async context =>
            {
                context.HttpContext.Response.ContentType = "text/plain";

                await context.HttpContext.Response.WriteAsync(
                    "Status code page, status code: " +
                    context.HttpContext.Response.StatusCode);
            });         */

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        public class ExceptionMiddleware
        {
            private readonly RequestDelegate _next;
            public ILogger _logger;

            public ExceptionMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
            {
                _next = next;
                _logger = loggerFactory.CreateLogger<ExceptionMiddleware>();
            }

            public async Task Invoke(HttpContext context)
            {
                var stopWatch = new Stopwatch();
                var request = "";
                var time = DateTime.Now;
                try
                {
                    
                    stopWatch.Start();
                   
                    request = context.Request.Method + "-"+ context.Request.Path;
                    await _next(context);
                    stopWatch.Stop();                    
                  //  _logger.LogInformation("Execution time of the request " + request + "is " + time);
                     Console.WriteLine("\n\n#####Execution time of the request " + request + "is " + time);

                }
                catch (Exception ex)
                {
                    stopWatch.Stop();
                    context.Response.ContentType = "text/plain";
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;


                    var rawWeather = JsonConvert.DeserializeObject<BcError>(ex.InnerException.Message);                    

                 //   _logger.LogInformation("\n\n#####Execution time of the request " + request + "is " + time);
                  Console.WriteLine("\n\n#####Execution time of the request " + request + "is " + time);
                   // _logger.LogError("\n\n####################"+ rawWeather.error.code + " " + "-" + " " + rawWeather.error.message + "\n\n\n");

                    await context.Response.WriteAsync(rawWeather.error.code + " " + "-" + " " + rawWeather.error.message);


                }
            }
        }
    }
}
