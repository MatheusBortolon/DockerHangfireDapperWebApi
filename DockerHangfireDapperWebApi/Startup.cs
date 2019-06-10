using Docker.Jobs;
using Docker.Jobs.Core;
using Hangfire;
using Hangfire.Dashboard;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Docker
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.RegisterServices(Configuration);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("Exemplo")));
            services.AddHangfireServer();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
        {
            GlobalConfiguration.Configuration.UseActivator(new HangfireActivator(serviceProvider));

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            loggerFactory.AddLog4Net();

            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseHangfireDashboard(options: new DashboardOptions()
            {
                Authorization = new[] { new HangFireAuthorizationFilter() }
            });
            
            RecurringJob.AddOrUpdate<IExemploJob>("IExemploJob", x => x.Perform(), "0/30 * * * * *");
        }

        public class HangFireAuthorizationFilter : IDashboardAuthorizationFilter
        {
            //Aqui tem que fazer uma lógica de autenticação
            public bool Authorize(DashboardContext context) =>
                true;
        }
    }
}
