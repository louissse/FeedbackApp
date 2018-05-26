using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using FeedbackApp.Repositories;
using Microsoft.EntityFrameworkCore;
using FeedbackApp.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using FeedbackApp.Data;

namespace FeedbackApp
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ISurveyService, SurveyService>();
            services.AddTransient<IFeedbackService, FeedbackService>();
            services.AddTransient<ICatRepository, CatRepository>();

            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<FeedbackContext>(options => options.UseSqlServer(connectionString));
            services.AddEntityFrameworkSqlServer()
                .AddDbContext<FeedbackContext>((serviceProvider, options) =>
                    options.UseSqlServer(connectionString)
                    .UseInternalServiceProvider(serviceProvider)
                );

            var dbContextOptionsbuilder = new DbContextOptionsBuilder<FeedbackContext>().UseSqlServer(connectionString);
            services.AddSingleton(dbContextOptionsbuilder.Options);

            services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });
            services.AddSession();
            services.AddMvc()
                .AddViewLocalization(
                    LanguageViewLocationExpanderFormat.Suffix,
                    opts => { opts.ResourcesPath = "Resources"; })
                .AddDataAnnotationsLocalization();

            services.Configure<RequestLocalizationOptions>(
                opts =>
                {
                    var supportedCultures = new List<CultureInfo>
                    {
                        new CultureInfo("en"),
                        new CultureInfo("da")
                    };

                    opts.DefaultRequestCulture = new RequestCulture("en");
                    // Formatting numbers, dates, etc.
                    opts.SupportedCultures = supportedCultures;
                    // UI strings that we have localized.
                    opts.SupportedUICultures = supportedCultures;
                });

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();

            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseSession();

            //Use localization
            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);

            app.UseStaticFiles(); //Enables the use of static files

            //Use Mvc
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            //Handle status code errors
            app.UseStatusCodePages("text/plain", "HTTP Error - Status Code: {0}");

        }
    }
}
