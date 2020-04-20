using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using LinqKit;

namespace JQ.Common.Infrastructure
{

    public static class ExpressionStarterExtensions
    {

        public static ExpressionStarter<TEntity> GetWhereExpression<TEntity, Parameters>(this ExpressionStarter<TEntity> expressionStarter, Parameters parameters)
        {
            var pe = Expression.Parameter(typeof(TEntity));
            var tType = typeof(TEntity);
            foreach (PropertyInfo property in typeof(Parameters).GetProperties())
            {
                var value = property.GetValue(parameters, null);
                if ((value != null)  && (tType.GetProperty(property.Name) != null))
                {
                    switch (GetNnumBinary(property))
                    {
                        case EnumBinaryExpression.Equal:
                            {
                                var memberExpression = Expression.PropertyOrField(pe, property.Name);
                                BinaryExpression expression = Expression.Equal(memberExpression, Expression.Convert(Expression.Constant(value), memberExpression.Type));
                                expressionStarter = expressionStarter.And(Expression.Lambda<Func<TEntity, bool>>(expression, pe));
                            }
                            break;
                        case EnumBinaryExpression.Contains:
                            {

                                var memberExpression = Expression.PropertyOrField(pe, property.Name);
                                MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                                ConstantExpression constant = Expression.Constant(value, typeof(string));
                                expressionStarter = expressionStarter.And(Expression.Lambda<Func<TEntity, bool>>(Expression.Call(memberExpression, method, constant), pe));
                            }
                            break;
                        case EnumBinaryExpression.GreaterThan:
                            {
                                var memberExpression = Expression.PropertyOrField(pe, property.Name);
                                var expression = Expression.GreaterThan(memberExpression, Expression.Convert(Expression.Constant(value), memberExpression.Type));
                                expressionStarter = expressionStarter.And(Expression.Lambda<Func<TEntity, bool>>(expression, pe));
                            }
                            break;
                        case EnumBinaryExpression.GreaterThanOrEqual:
                            {
                                var memberExpression = Expression.PropertyOrField(pe, property.Name);
                                var expression = Expression.GreaterThanOrEqual(memberExpression, Expression.Convert(Expression.Constant(value), memberExpression.Type));
                                expressionStarter = expressionStarter.And(Expression.Lambda<Func<TEntity, bool>>(expression, pe));
                            }
                            break;
                        case EnumBinaryExpression.LessThan:
                            {
                                var memberExpression = Expression.PropertyOrField(pe, property.Name);
                                var expression = Expression.LessThan(memberExpression, Expression.Convert(Expression.Constant(value), memberExpression.Type));
                                expressionStarter = expressionStarter.And(Expression.Lambda<Func<TEntity, bool>>(expression, pe));
                            }
                            break;
                        case EnumBinaryExpression.LessThanOrEqual:
                            {
                                var memberExpression = Expression.PropertyOrField(pe, property.Name);
                                var expression = Expression.LessThanOrEqual(memberExpression, Expression.Convert(Expression.Constant(value), memberExpression.Type));
                                expressionStarter = expressionStarter.And(Expression.Lambda<Func<TEntity, bool>>(expression, pe));
                            }
                            break;
                            //case EnumBinaryExpression.In:
                            //    {
                            //        var value = property.GetValue(parameters, null);
                            //        if (value != null)
                            //        {

                            //            var valueArr = new List<string>(value.ToString().Split(','));
                            //            var memberExpression = Expression.PropertyOrField(pe, property.Name);
                            //            Expression expression = Expression.Constant(true, typeof(bool));

                            //            foreach (var itemVal in valueArr)
                            //            {

                            //                Expression constant = Expression.Constant(itemVal);
                            //                Expression right = Expression.Equal(memberExpression, Expression.Convert(constant, memberExpression.Type));

                            //                expression = Expression.Or(expression, right);

                            //            }
                            //            expressionStarter = expressionStarter.And(Expression.Lambda<Func<TEntity, bool>>(expression, pe));

                            //        }
                            //    }

                            //    break;
                    }

                }
                
         
                    
                
                   

            }

            return expressionStarter;
        }

        static EnumBinaryExpression GetNnumBinary(PropertyInfo property)
        {
            var binary = (BinaryExpressionAttribute)Attribute.GetCustomAttribute(property, typeof(BinaryExpressionAttribute));
            return binary != null ? binary.EnumBinaryExp : EnumBinaryExpression.Null;
        }
    }

    //重构版，存在问题待排查
    //public static class ExpressionStarterExtensions
    //{

    //    public static ExpressionStarter<TEntity> GetWhereExpression<TEntity, Parameters>(this ExpressionStarter<TEntity> expressionStarter, Parameters parameters)
    //    {
    //        var pe = Expression.Parameter(typeof(TEntity));
    //        Expression expression = null;
    //        foreach (PropertyInfo property in typeof(Parameters).GetProperties())
    //        {
    //            var value = property.GetValue(parameters, null);
    //            if (value != null)
    //            {
    //                var member = Expression.PropertyOrField(pe, property.Name);
    //                var constant = Expression.Constant(value, typeof(string));

    //                switch (GetEnumBinary(property))
    //                {
    //                    case EnumBinaryExpression.Equal:
    //                        expression = Expression.Equal(member, constant);
    //                        break;
    //                    case EnumBinaryExpression.Contains:
    //                        MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
    //                        expression = Expression.Call(member, method, constant);
    //                        break;
    //                    case EnumBinaryExpression.GreaterThan:
    //                        expression = Expression.GreaterThan(member, constant);
    //                        break;
    //                    case EnumBinaryExpression.GreaterThanOrEqual:
    //                        expression = Expression.GreaterThanOrEqual(member, constant);
    //                        break;
    //                    case EnumBinaryExpression.LessThan:
    //                        expression = Expression.LessThan(member, constant);
    //                        break;
    //                    case EnumBinaryExpression.LessThanOrEqual:
    //                        expression = Expression.LessThanOrEqual(member, constant);
    //                        break;
    //                }

    //                expressionStarter = expressionStarter.And(Expression.Lambda<Func<TEntity, bool>>(expression, pe));
    //            }
    //        }

    //        return expressionStarter;
    //    }

    //    static EnumBinaryExpression GetEnumBinary(PropertyInfo property)
    //    {
    //        var binary = (BinaryExpressionAttribute)Attribute.GetCustomAttribute(property, typeof(BinaryExpressionAttribute));
    //        return binary != null ? binary.EnumBinaryExp : EnumBinaryExpression.Null;
    //    }
    //}
}
