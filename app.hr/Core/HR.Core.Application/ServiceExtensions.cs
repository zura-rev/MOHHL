﻿using FluentValidation;
using HR.Core.Application.Features.Employees.Commands;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace HR.Core.Application
{
    public static class ServiceExtensions
    {
        public static void AddApplicatonLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());


            //services.AddScoped<CreateEmployee>();
        }
    }
}
