using FluentValidation.AspNetCore;
using Hl.Core.Application;
using Hl.Core.Application.Interfaces.Contracts;
using Hl.Infrastructure.Persistence;
using Hl.Presentation.Admin.Extensions.Middlewares;
using Hl.Presentation.Extensions.Services;
using Hl.Presentation.WebApi.Extensions.Middlewares;
using Hl.Presentation.WebApi.Extensions.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Hl.Presentation.WebApi
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

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HL API", Version = "v1" });
            });

            services.AddCors(options =>
            {
                options.AddPolicy(name: "_myPolicy", builder => builder
                    .AllowAnyMethod() // დაშვებას იძლევა HTTP ყველა მეთოდზე
                    .AllowAnyHeader()
                    .AllowAnyOrigin() // დაშვება ეძლევა მოთხოვნას ნებისმიერი წყაროდან
                    .WithExposedHeaders("Authorization", "AccessToken", "PageIndex", "PageSize", "TotalPages", "TotalCount", "HasPreviousPage", "HasNextPage"));
            });

            services.AddApplicatonLayer(Configuration);
            services.AddPersistenceLayer(Configuration);
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddSingleton<IActiveObjectsService, ActiveObjectsService>();

            services.AddJwtAuthenticationConfigs(Configuration);
            services.AddJwtAuthorizationConfigs();
            //services.AddSingleton(new ActiveObjectsService(3000));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HL API v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseCors("_myPolicy");
           
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
