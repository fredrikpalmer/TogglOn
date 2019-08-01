using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StructureMap;
using System;
using TogglOn.Client.Abstractions.Builder;
using TogglOn.Client.AspNetCore.Builder;
using TogglOn.Core.Configuration;
using TogglOn.DependencyInjection.AspNetCore;

namespace TogglOn.AspNetCoreClient
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddHttpContextAccessor();

            services
                .AddTogglOnCore(options => options.UseMongoDb("mongodb://localhost:27017"))
                .AddClient(options => options.UseInProcClient());
            
            var container = new Container();
            
            container.Populate(services);

            return container.GetInstance<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseTogglOnClient(togglOn =>
            {
                togglOn.DeclareNamespace("DevOps");
                togglOn.DeclareEnvironment(env.EnvironmentName);
                togglOn.DeclareFeatureGroups(FeatureGroups);
                togglOn.DeclareFeatureToggles(FeatureToggles);
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void FeatureGroups(IFeatureGroupBuilder builder)
        {
            builder
                .WithGroup("office")
                .WithCustomerIds("1")
                .WithClientIps("::1");
        }

        private void FeatureToggles(IFeatureToggleBuilder builder)
        {
            builder
                .WithToggle("my-awesome-feature", true)
                .WhenAny(toggle =>
                {
                    toggle.WithFeatureGroup("office");
                    toggle.WithPercentage(50);
                });
        }
    }
}
