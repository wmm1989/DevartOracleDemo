using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JQ.Common.Infrastructure
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method,AllowMultiple =true,Inherited =true)]
    public class CustomRouteAttribute : RouteAttribute, IApiDescriptionGroupNameProvider
    {
        public string GroupName { get; set; }

        public CustomRouteAttribute(string actionName = "[action]") : base("/api/{version}/[controller]/" + actionName)
        {
        }

        public CustomRouteAttribute(EnumApiVersion version,string actionName = "[action]"):base($"/api/{version.ToString()}/[Controller]/{actionName}")
        {
            GroupName = version.ToString();
        }

    }

    public enum EnumApiVersion
    {
        v1 = 1
    }
}
