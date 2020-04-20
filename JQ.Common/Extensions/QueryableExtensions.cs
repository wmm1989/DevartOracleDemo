using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using JQ.Common.Model;

namespace JQ.Common.Infrastructure
{
    public static class QueryableExtensions
    {
        private static PropertyInfo GetPropertyInfo(Type objType, string name)
        {
            var properties = objType.GetProperties();
            var matchedProperty = properties.FirstOrDefault(p => p.Name.ToUpper() == name.ToUpper());
            //if (matchedProperty == null)
            //{
            //    throw new ArgumentException("name");
            //}

            return matchedProperty;
        }
        private static LambdaExpression GetOrderExpression(Type objType, PropertyInfo pi)
        {
            var paramExpr = Expression.Parameter(objType);
            var propAccess = Expression.PropertyOrField(paramExpr, pi.Name);
            var expr = Expression.Lambda(propAccess, paramExpr);
            return expr;

        }





        public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, string orderBy)
        {
            if (string.IsNullOrEmpty(orderBy))
            {
                return query;
            }

            orderBy = orderBy.Trim();


            var _method = "OrderBy";
            if (orderBy.StartsWith("-"))
            {
                _method = "OrderByDescending";
            }

            var propInfo = GetPropertyInfo(typeof(T), orderBy.Replace("-", ""));
            if (propInfo != null)
            {
                var expr = GetOrderExpression(typeof(T), propInfo);
                var method = typeof(Queryable).GetMethods().FirstOrDefault(mt => mt.Name == _method && mt.GetParameters().Length == 2);
                var genericMethod = method.MakeGenericMethod(typeof(T), propInfo.PropertyType);
                query = (IQueryable<T>)genericMethod.Invoke(null, new object[] { query, expr });
            }


            return query;

        }

        public static IQueryable<T> OrderByBatch<T>(this IQueryable<T> query, string orderBy)
        {
            if (string.IsNullOrEmpty(orderBy))
            {
                return query;
            }
            var index = 0;
            var orderByArr = orderBy.Split(',');

            foreach (var item in orderByArr)
            {
                var m = index++ > 0 ? "ThenBy" : "OrderBy";
                if (item.StartsWith("-"))
                {
                    m += "Descending";
                    orderBy = item.Substring(1);
                }
                else
                {
                    orderBy = item.TrimStart('+');
                }
                orderBy = orderBy.Trim();

                var propInfo = GetPropertyInfo(typeof(T), orderBy);
                if (propInfo != null)
                {
                    var expr = GetOrderExpression(typeof(T), propInfo);
                    var method = typeof(Queryable).GetMethods().FirstOrDefault(mt => mt.Name == m && mt.GetParameters().Length == 2);
                    var genericMethod = method.MakeGenericMethod(typeof(T), propInfo.PropertyType);
                    query = (IQueryable<T>)genericMethod.Invoke(null, new object[] { query, expr });
                }

            }
            return query;

        }

        public static IQueryable<T> ApplySort<T>(this IQueryable<T> source, string orderBy, IPropertyMapping propertyMapping)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var mappingDictionary = propertyMapping.MappingDictionary;
            if (mappingDictionary == null)
            {
                throw new ArgumentNullException(nameof(mappingDictionary));
            }

            if (string.IsNullOrWhiteSpace(orderBy))
            {
                return source;
            }

            var orderByAfterSplit = orderBy.Split(',');
            foreach (var orderByClause in orderByAfterSplit.Reverse())
            {
                var trimmedOrderByClause = orderByClause.Trim();

                var orderDescending = trimmedOrderByClause.StartsWith("-");

                var propertyName = orderDescending ? trimmedOrderByClause.Replace("-", "") : trimmedOrderByClause.Replace("+", "");

                if (!mappingDictionary.ContainsKey(propertyName))
                {
                    throw new ArgumentException($"未映射字段{propertyName}");
                }
                var mappedProperties = mappingDictionary[propertyName];
                if (mappedProperties == null)
                {
                    throw new ArgumentNullException(propertyName);
                }
                mappedProperties.Reverse();
                foreach (var destinationProperty in mappedProperties)
                {
                    if (destinationProperty.Revert)
                    {
                        orderDescending = !orderDescending;
                    }
                    source = source.OrderBy(destinationProperty.Name + (orderDescending ? " descending" : " ascending"));
                }
            }

            return source;
        }

        public static IQueryable<object> ToDynamicQueryable<TSource>
            (this IQueryable<TSource> source, string fields, Dictionary<string, List<MappedProperty>> mappingDictionary)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (mappingDictionary == null)
            {
                throw new ArgumentNullException(nameof(mappingDictionary));
            }

            if (string.IsNullOrWhiteSpace(fields))
            {
                return (IQueryable<object>)source;
            }

            fields = fields.ToLower();
            var fieldsAfterSplit = fields.Split(',').ToList();
            if (!fieldsAfterSplit.Contains("id", StringComparer.InvariantCultureIgnoreCase))
            {
                fieldsAfterSplit.Add("id");
            }
            var selectClause = "new (";

            foreach (var field in fieldsAfterSplit)
            {
                var propertyName = field.Trim();
                if (!mappingDictionary.ContainsKey(propertyName))
                {
                    throw new ArgumentException($"{propertyName}不存在");
                }
                var mappedProperties = mappingDictionary[propertyName];
                if (mappedProperties == null)
                {
                    throw new ArgumentNullException(propertyName);
                }
                foreach (var destinationProperty in mappedProperties)
                {
                    selectClause += $" {destinationProperty.Name},";
                }
            }

            selectClause = selectClause.Substring(0, selectClause.Length - 1) + ")";
            return (IQueryable<object>)source.Select(selectClause);
        }

    }
}
