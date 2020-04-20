using System;
using System.Collections.Generic;
using System.Text;

namespace JQ.Common.Infrastructure
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public class BinaryExpressionAttribute : Attribute
    {
        public EnumBinaryExpression EnumBinaryExp { get; set; }
        public BinaryExpressionAttribute(EnumBinaryExpression enumBinaryExpression)
        {
            EnumBinaryExp = enumBinaryExpression;
        }
    }

    public enum EnumBinaryExpression
    {
        Null = 0,
        Equal = 1,
        Contains = 2,
        GreaterThan = 3,
        GreaterThanOrEqual = 4,
        LessThan = 5,
        LessThanOrEqual = 6,
       // In=7
    }


}
