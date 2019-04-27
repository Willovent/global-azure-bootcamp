using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Todo.Initializers;
using Todo.Data;
using Microsoft.ApplicationInsights.Extensibility;
using Todo.Core.Clients;
using Todo.Core.Initializers;

namespace Todo
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
            services.AddDbContext<TodoContext>(opts => opts.UseSqlServer(Configuration.GetConnectionString("Context")));
            services.AddHttpContextAccessor();
            services.AddSingleton<ITelemetryInitializer, CorrelationIdTelemetryInitializer>();
            services.AddSingleton<ITelemetryInitializer, AppNameInitializer>(_ => new AppNameInitializer("Todo.Api"));
            services.AddSingleton(_ => new TodoQueueClient(Configuration.GetConnectionString("StorageAccount")));
            services.AddApplicationInsightsTelemetry();
            services.AddMvc();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors(configurePolicy =>
            {
                configurePolicy.AllowAnyHeader()
                .AllowAnyOrigin()
                .AllowAnyMethod();
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
