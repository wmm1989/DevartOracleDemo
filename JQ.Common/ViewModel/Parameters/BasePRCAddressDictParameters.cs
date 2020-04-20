using System;
using JQ.Common.Infrastructure;
using JQ.Common.Model;

namespace JQ.Common.ViewModel
{
    public class BasePRCAddressDictParameters : PaginationBase
    {
		
           /// <summary>
           /// Desc:行政区划编号
           /// Default:
           /// Nullable:True
           /// </summary>
           [BinaryExpression(EnumBinaryExpression.Equal)]
           public string AreaNumber {get;set;}
           /// <summary>
           /// Desc:行政区划名称
           /// Default:
           /// Nullable:True
           /// </summary>
           [BinaryExpression(EnumBinaryExpression.Contains)]
           public string AreaName {get;set;}
           /// <summary>
           /// Desc:上级行政区域编号
           /// Default:
           /// Nullable:True
           /// </summary>
           [BinaryExpression(EnumBinaryExpression.Equal)]
           public string SuperiorAreaNumber {get;set;}
           /// <summary>
           /// Desc:行政区划级别 
           /// Default:
           /// Nullable:True
           /// </summary>
           [BinaryExpression(EnumBinaryExpression.Equal)]
           public string AreaLv {get;set;}
           /// <summary>
           /// Desc:拼音码
           /// Default:
           /// Nullable:True
           /// </summary>
           [BinaryExpression(EnumBinaryExpression.Contains)]
           public string PYM {get;set;}
           /// <summary>
           /// Desc:五笔码
           /// Default:
           /// Nullable:True
           /// </summary>
           [BinaryExpression(EnumBinaryExpression.Contains)]
           public string WBM {get;set;}


    }
}


