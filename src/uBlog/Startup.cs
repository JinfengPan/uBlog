using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.IO;
using uBlog.Options;
using Microsoft.Extensions.Logging;
using uBlog.Services;
using uBlog.IServices;
using uBlog.Repository;

namespace uBlog
{
    public class Startup
    {
        private IHostingEnvironment hostingEnvironment;

        public IConfiguration configuration { get; set; }

        public Startup(IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            this.configuration = builder.Build();

        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("alertThresholds.json")
                .AddJsonFile($"alertThresholds{hostingEnvironment.EnvironmentName}.json", optional: true);

            var config = configBuilder.Build();
            services.Configure<ThresholdOptions>(op =>
            {
                op.WaterTemperatureMax = Int32.Parse(config["WaterTemperatureMax"]);
                op.WaterTemperatureMin = Int32.Parse(config["WaterTemperatureMin"]);
            });

            services.AddMvc();
            services.AddSingleton<ISensorDataService, SensorDataService>();
            services.AddSingleton<IBlogPostService, BlogPostService>();
            services.AddSingleton(typeof(UBlogContext), 
                new UBlogContext(
                    configuration["MongoDBCollectionString"],
                    configuration["MondoDBNames:uBlog"]
                ));


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            if(hostingEnvironment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var logger = loggerFactory.CreateLogger("info");

            app.UseRuntimeInfoPage("/info");
            app.UseDefaultFiles();
            app.UseStaticFiles();


            //要设定接受来自哪个授权服务器的access_token
            app.UseJwtBearerAuthentication(new JwtBearerOptions {

            });

            app.UseMvcWithDefaultRoute();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
