using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace JQ.Common.Infrastructure
{
    public static class AutoMapperExtensions
    {
        public static void AddAutoMapperAndMappings(this IServiceCollection services)
        {
            services.AddAutoMapper();
        }
    }
}
