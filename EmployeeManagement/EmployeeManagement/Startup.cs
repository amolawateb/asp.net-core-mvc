using EmployeeManagement.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace EmployeeManagement
{
    public class Startup
    {
        private IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();//add all MVC services including MVC core, good to use
            //services.AddMvcCore();//only add MVC code services

            services.AddScoped<IEmployeeRepository, SqlEmployeeRepo>();//use to register the services dependency injection
            services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(_config.GetConnectionString("EmployeeDBConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                //DeveloperExceptionPageOptions developerExceptionPageOptions = new DeveloperExceptionPageOptions()
                //{
                //    SourceCodeLineCount = 1 //use to display line of code before and after eception
                //};

                //app.UseDeveloperExceptionPage(developerExceptionPageOptions);
                app.UseDeveloperExceptionPage();
            }

            //app.Use(async (context, next) =>
            //{
            //    logger.LogInformation("Mw1: Incoming Request");
            //    //await context.Response.WriteAsync("Hello World From 1st Middleware!!\n");
            //    //await context.Response.WriteAsync(System.Diagnostics.Process.GetCurrentProcess().ProcessName + "\n");
            //    //await context.Response.WriteAsync(_config["MyKey"] + "\n");
            //    await next();
            //    logger.LogInformation("Mw1: Outgoing Request");
            //});

            //app.Use(async (context, next) =>
            //{
            //    logger.LogInformation("Mw2: Incoming Request");
            //    await next();
            //    logger.LogInformation("Mw2: Outgoing Request");
            //});

            //>>>>>>>>>use to display the default file such as default.html, index.html<<<<<<<<<<<<<<<<<<<
            //DefaultFilesOptions defaultFilesOptions = new DefaultFilesOptions(); 
            //defaultFilesOptions.DefaultFileNames.Clear();
            //defaultFilesOptions.DefaultFileNames.Add("foo.html");
            //app.UseDefaultFiles(defaultFilesOptions); //Will serve static files from wwwroot folder such as html

            //>>>>>>>>>use to display the non default file such as foo.html<<<<<<<<<<<<<<<<<<<<<<<<
            //FileServerOptions fileServerOptions = new FileServerOptions(); 
            //fileServerOptions.DefaultFilesOptions.DefaultFileNames.Clear();
            //fileServerOptions.DefaultFilesOptions.DefaultFileNames.Add("foo.html");
            //app.UseFileServer(fileServerOptions);

            app.UseStaticFiles(); //Will serve static files from wwwroot folder such as images js, css


            //app.UseMvcWithDefaultRoute();  //add MVC suport with default route method

            //add MVC support without default rotre method
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

            //app.UseMvc();

            //app.Run(async (context) =>
            //{
                //throw new Exception("Some error processing the request");
                //await context.Response.WriteAsync("Hello World From 3rd Middelware!!\n");
                //await context.Response.WriteAsync("Hello World");
                //logger.LogInformation("Mw3: Outgoing Request");
            //});
        }
    }
}
