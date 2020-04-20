using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using JQ.Common.Helpers;
using JQ.Common.Infrastructure;
using JQ.Common.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;

namespace JQ.Common.Repository
{
    public class ApplicationDbContext : DbContext
    {
        public static readonly LoggerFactory BaseLoggerFactory
           = new LoggerFactory(new[] { new DebugLoggerProvider((_, __) => true) });


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var loggerFactory = new LoggerFactory();
            //loggerFactory.AddProvider(new EFLoggerProvider());
            //optionsBuilder.UseLoggerFactory(loggerFactory);

            base.OnConfiguring(optionsBuilder);
            optionsBuilder.EnableSensitiveDataLogging();
            // optionsBuilder.UseLoggerFactory(BaseLoggerFactory);

           
           

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string path = AppContext.BaseDirectory;

            if (FileHelper.IsExistFile(path + "JQ.Common.dll"))
            {
                var entityTypes = Assembly.Load(new AssemblyName("JQ.Common")).GetTypes()
                    .Where(type => !string.IsNullOrWhiteSpace(type.Namespace))
                    .Where(type => type.GetTypeInfo().IsClass)
                    .Where(type => type.GetTypeInfo().BaseType != null && (type.GetTypeInfo().BaseType == typeof(Entity)))
                    //.Where(type => typeof(IEntity).IsAssignableFrom(type))
                    .ToList();

                SetDel(modelBuilder, entityTypes);
            }


            if (FileHelper.IsExistFile(path + "JQ.XXX.Model.dll"))
            {
                var entityTypes = Assembly.Load(new AssemblyName("JQ.XXX.Model")).GetTypes()
                    .Where(type => !string.IsNullOrWhiteSpace(type.Namespace))
                    .Where(type => type.GetTypeInfo().IsClass)
                    .Where(type => type.GetTypeInfo().BaseType != null && (type.GetTypeInfo().BaseType == typeof(Entity)))
                    //.Where(type => typeof(IEntity).IsAssignableFrom(type))
                    .ToList();

                SetDel(modelBuilder, entityTypes);
            }

            base.OnModelCreating(modelBuilder);
        }


        void SetDel(ModelBuilder modelBuilder, List<Type> entityTypes)
        {
            foreach (var entityType in entityTypes)
            {
                modelBuilder.Model.GetOrAddEntityType(entityType);

                //if (entityType.GetTypeInfo().BaseType == typeof(Entity))
                //{
                //    modelBuilder.Entity(entityType).Property("ID").ForOracleUseSequenceHiLo(entityType.Name.Replace("_", "").ToUpper() + "_SEQ");
                //}

                if (entityType.GetProperties().FirstOrDefault(p => p.Name == "IsDel") != null)
                {
                    var parameter = Expression.Parameter(entityType);
                    var propertyMethodInfo = typeof(EF).GetMethod("Property").MakeGenericMethod(typeof(int));
                    var isDeleteProperty = Expression.Call(propertyMethodInfo, parameter, Expression.Constant("IsDel"));
                    BinaryExpression compareExpression = Expression.MakeBinary(ExpressionType.Equal, isDeleteProperty, Expression.Constant(0));
                    var lambdaExpression = Expression.Lambda(compareExpression, parameter);

                    modelBuilder.Entity(entityType).HasQueryFilter(lambdaExpression);
                }

                else
                {
                    //ValueConverter converter = new ValueConverter<string, string>(v => v, v => v.ToUpper());
                    //foreach (var item in entityType.GetProperties())
                    //{
                    //    if (item.Name == "PYM")
                    //    {
                    //        modelBuilder.Entity(entityType).Property("PYM").HasConversion(converter);
                    //    }
                    //}

                    modelBuilder.Entity(entityType);
                }

            }
        }

    }
}
