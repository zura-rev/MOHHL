using FluentValidation.AspNetCore;
using HR.Core.Application;
using HR.Core.Application.Interfaces.Contracts;
using HR.Infrastructure.Persistence;
using HR.Presentation.Admin.Extensions.Middlewares;
using HR.Presentation.Extensions.Services;
using HR.Presentation.WebApi.Extensions.Middlewares;
using HR.Presentation.WebApi.Extensions.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HR.Presentation.WebApi
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
            services.AddControllers()
                .AddFluentValidation()
                .AddNewtonsoftJson(x =>
            {
                x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            services.AddSwaggerGen();

            services.AddApplicatonLayer(Configuration);
            services.AddPersistenceLayer(Configuration);
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddJwtAuthenticationConfigs(Configuration);
            services.AddJwtAuthorizationConfigs();
            services.AddSingleton(new ActiveObjectsService(3000));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HL v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseMiddleware<ExceptionHandler>();
            app.UseMiddleware<UserCachingMiddlewares>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
