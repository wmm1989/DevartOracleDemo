using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace JQ.Base.Infrastructure
{
    public class CorsPolicy
    {
        public List<CorsPolicyParam> CorsList { get; private set; }
        public CorsPolicy()
        {
            CorsList = new List<CorsPolicyParam>()
            {
                new CorsPolicyParam(){ Name="managerCors",Origins="http://localhost:8092".TrimEnd('\\')}
            };

            
        }

        public List<string> GetNameList()
        {
            return CorsList.Select(c => c.Name).ToList();
        }

        public void AddPolicys(CorsOptions cors)
        {
            foreach (var param in CorsList)
            {
                cors.AddPolicy(param.Name, policy =>
                {
                    policy
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
                ;
                });
            }
        }
    }

    public class CorsPolicyParam
    {
        public string Name { get; set; }

        public string Origins { get; set; }
    }
}
