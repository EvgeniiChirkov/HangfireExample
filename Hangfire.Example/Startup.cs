using System;
using Hangfire.Example.Configuration;
using Hangfire.Example.Services;
using Hangfire.Example.Tasks;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hangfire.Example
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
            services.Configure<ServiceSettings>(Configuration.GetSection("ServiceSettings"));
            services.Configure<TemplateSynchronizationSettings>(Configuration.GetSection("TemplateSynchronizationSettings"));

            services.AddSingleton<ISampleService, SampleService>();
            services.AddSingleton<ITaskExecutor, HangfireTaskExecutor>();
            services.AddSingleton<IJobManager, JobManager>();
            services.AddSingleton<SampleJob>();
            services.AddSingleton<IHangFireFilter, HangFireFilter>();
            services.AddSingleton<IHangfireJobMemory, HangfireJobMemory>();
            
           
            GlobalConfiguration.Configuration
                .UseActivator(new HangfireActivator(services.BuildServiceProvider()));
            GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute { Attempts = 0 });
            services.AddHangfire((provider, configuration) =>
            {
                configuration.UsePostgreSqlStorage(Configuration.GetSection("HangfireSettings")["ConnectionString"]);
                configuration.UseFilter(provider.GetService<IHangFireFilter>());
            });
  
            services.AddHangfireServer();
            services.AddControllers();
            
            TaskInformant.FillTasks(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            IServiceProvider serviceProvider)
        {
            GlobalConfiguration.Configuration
                .UseActivator(new HangfireActivator(serviceProvider));
            
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseHangfireDashboard();
        }
    }
}
