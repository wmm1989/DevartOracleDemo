using System;
using JQ.Common.Infrastructure;
using JQ.Common.Model;

namespace JQ.Common.ViewModel
{
    public class BaseFeeItemClassParameters : PaginationBase
    {
		
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>
           [BinaryExpression(EnumBinaryExpression.Equal)]
           public int? ID {get;set;}
      
           /// <summary>
           /// Desc:ID
           /// Default:
           /// Nullable:True
           /// </summary>
           [BinaryExpression(EnumBinaryExpression.Equal)]
           public string TreeID {get;set;}
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>
           [BinaryExpression(EnumBinaryExpression.Equal)]
           public string TreePID {get;set;}
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>
           [BinaryExpression(EnumBinaryExpression.Equal)]
           public string IncomeProjectClassID {get;set;}
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>
           [BinaryExpression(EnumBinaryExpression.Equal)]
           public string IncomeProjectClassName {get;set;}
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>
           [BinaryExpression(EnumBinaryExpression.Contains)]
           public string PYM {get;set;}
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>
           [BinaryExpression(EnumBinaryExpression.Contains)]
           public string WBM {get;set;}


    }
}


