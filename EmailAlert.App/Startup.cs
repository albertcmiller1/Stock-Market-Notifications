using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
//using EmailAlert.Data.InMemoryAccess;
using EmailAlert.Data.DbService;
using Microsoft.EntityFrameworkCore;
using EmailAlert.Data;
using EmailAlert.Data.Interfaces;
using EmailAlert.Business.StockService;
using EmailAlert.Business;
using System.Timers;
using System;

namespace EmailAlert.App
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });



            //services.AddSingleton<IEmailDbAccess, EmailMemAccess>();
            //services.AddSingleton<IRegisterDbAccess, RegisterMemAccess>();
            //services.AddScoped<IAdminDbAccess, AdminMemAccess>();



            services.AddScoped<IStockService, StockService>();

            services.AddScoped<IEmailDbAccess, EmailDbAccess>();
            services.AddScoped<IRegisterDbAccess, RegisterDbAccess>();
            services.AddScoped<IAdminDbAccess, AdminDbAccess>();

            services.AddScoped<EmailAPI>();
            services.AddScoped<AnalyzeStocks>();
            services.AddScoped<ParseUsersStocks>();
            //services.AddScoped<SendRecurringEmails>();

            
            services.AddDbContextPool<EmailAlertDbContext>(options =>
            {
                options.UseSqlServer("Data Source=(localdb)\\mssqllocaldb; Initial Catalog = EmailAlertDB; Integrated Security=True;");
            });

            
            //...................................................................................


            //services.AddScoped<SendRecurringEmails>();
            //services.AddHostedService<ConsumeScopedServiceHostedService>();


            //...................................................................................
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
