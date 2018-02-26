using Bespoke.Cloud.CustomersTest.Business;
using Bespoke.Cloud.CustomersTest.Business.Interfaces;
using Bespoke.Cloud.CustomersTest.Repository;
using Bespoke.Cloud.CustomersTest.Repository.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using AutoMapper;

namespace Bespoke.Cloud.CustomersTest
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
            services.AddCors();
            services.AddMvc(setupAction =>
            {
                // (^_^): Return 406 for Non JSON request
                setupAction.ReturnHttpNotAcceptable = true;
                // (^_^): Accept header for XML allowed
                setupAction.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
                // (^_^): XML INPUT formatter too
                setupAction.InputFormatters.Add(new XmlDataContractSerializerInputFormatter());
            });
            services.AddAutoMapper();

            // register services
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerManager, CustomerManager>();

            services.AddSingleton(_ => Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
           ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            loggerFactory.AddDebug(LogLevel.Information);

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // EW (^_^): Production mode error handling
                app.UseExceptionHandler(appBuilder =>
                {
                    // EW: Clean 500 msg for Production
                    appBuilder.Run(async context =>
                    {

                        var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                        if (exceptionHandlerFeature != null)
                        {
                            var logger = loggerFactory.CreateLogger("Global exception logger");
                            logger.LogError(500,
                                exceptionHandlerFeature.Error,
                                exceptionHandlerFeature.Error.Message);
                        }


                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("There appears to have been a dumb error (>_<)");
                    });
                });
            }

            app.UseMvc();
        }
    }
}

