using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using FluentValidation.AspNetCore;
using JQ.Common.Helpers;
using JQ.Common.Infrastructure;
using JQ.Common.IRepository;
using JQ.Base.Infrastructure;
using JQ.Common.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using NLog.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using JQ.Base.IServices;
using Swashbuckle.AspNetCore.SwaggerUI;
using NLog.Web;

namespace JQ.Base.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = true;
            })
            .AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss.fff";
            })
             .AddFluentValidation()
             .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            ;

            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                var dbType = AppSettingHelper.App("AppSettings", "SqlDb", "DbType");
                var conn = AppSettingHelper.App("AppSettings", "SqlDb", "Connection");


                switch (dbType)
                {
                    case "1":
                        options.UseSqlServer(
                        conn
                        );
                        break;
                    case "2":
                        options.UseOracle(conn, b => b.UseOracleSQLCompatibility("11"));
                        break;
                    case "3":
                        {
                            Action<Devart.Data.Oracle.Entity.OracleDbContextOptionsBuilder> action = null;
                            options.UseOracle(conn, action);
                        }
                        break;
                }

            });

            services.AddAutoMapperAndMappings();




            services.AddCors(c =>
            {
                CorsPolicy cors = new CorsPolicy();
                cors.AddPolicys(c);
            });



            services.AddOptions();




            var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;

            #region swagger

            string apiTitle = AppSettingHelper.App("Service", "Title");
            string description = AppSettingHelper.App("Service", "Description");

            services.AddSwaggerGen(context =>
            {
                #region swagger注册

                typeof(EnumApiVersion).GetEnumNames().ToList().ForEach(version =>
                {
                    context.SwaggerDoc(version, new Info()
                    {
                        Title = $"{apiTitle} Api {version} 文档",
                        Version = version,
                        Description = description
                    });
                });
                #endregion



                #region  输入token
                //手动高亮
                //添加header验证信息
                //c.OperationFilter<SwaggerHeader>();
                var security = new Dictionary<string, IEnumerable<string>> { { "Bearer", new string[] { } }, };
                context.AddSecurityRequirement(security);//添加一个必须的全局安全信息，和AddSecurityDefinition方法指定的方案名称要一致，这里是Bearer。
                context.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 参数结构: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",//jwt默认的参数名称
                    In = "header",//jwt默认存放Authorization信息的位置(请求头中)
                    Type = "apiKey"
                });
                #endregion

                #region  读取xml 添加注释

                var xmlPath = Path.Combine(basePath, "JQ.Base.Api.xml");
                context.IncludeXmlComments(xmlPath, true);

                var xmlModelPath = Path.Combine(basePath, "JQ.Common.xml");
                context.IncludeXmlComments(xmlModelPath, true);

                context.AddFluentValidationRules();

                #endregion


            });
            #endregion

            #region

            //实例化AutoFac 容器生成器
            var builder = new ContainerBuilder();

            builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IBaseRepository<>)).InstancePerDependency();//生命周期 默认，未使用单例模式SingleInstance

            var servicesCommonDllFile = Path.Combine(basePath, "JQ.Common.dll");//获取注入项目绝对路径
            var assemblysServicesCommon = Assembly.LoadFile(servicesCommonDllFile);

            var servicesDllFile = Path.Combine(basePath, "JQ.Base.Services.dll");//获取注入项目绝对路径
            var assemblysServices = Assembly.LoadFile(servicesDllFile);

            builder.RegisterAssemblyTypes(assemblysServicesCommon, assemblysServices).AsImplementedInterfaces()
               .InstancePerLifetimeScope()
               .EnableInterfaceInterceptors() 
               ;


            //填充到容器生成器
            builder.Populate(services);
            //创建容器
            var ApplicationContainer = builder.Build();

            #endregion



            return new AutofacServiceProvider(ApplicationContainer);//第三方IOC接管 core内置DI容器
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime lifetime)
        {



            loggerFactory.AddNLog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            #region Swagger
            app.UseSwagger();
            app.UseSwaggerUI(context =>
            {
                typeof(EnumApiVersion).GetEnumNames().OrderByDescending(a => a).ToList().ForEach(version =>
                {
                    context.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"{version}");
                });
                context.DocExpansion(DocExpansion.None);
                context.RoutePrefix = ""; //路径设置，为空则默认直接显示
            });
            #endregion

            #region Cors
            CorsPolicy cors = new CorsPolicy();
            foreach (var name in cors.GetNameList())
            {
                app.UseCors(name);
            }
            #endregion

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
