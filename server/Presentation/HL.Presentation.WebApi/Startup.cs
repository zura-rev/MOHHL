using FluentValidation.AspNetCore;
using HL.Core.Application;
using HL.Core.Application.Interfaces.Contracts;
using HL.Infrastructure.Persistence;
using HL.Presentation.Admin.Extensions.Middlewares;
using HL.Presentation.Extensions.Services;
using HL.Presentation.WebApi.Extensions.Middlewares;
using HL.Presentation.WebApi.Extensions.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HL.Presentation.WebApi
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
