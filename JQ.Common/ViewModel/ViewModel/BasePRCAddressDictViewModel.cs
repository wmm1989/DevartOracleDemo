using System;

namespace JQ.Common.ViewModel
{
    public class BasePRCAddressDictViewModel
    {
		
           /// <summary>
           /// Desc:行政区划编号
           /// Default:
           /// Nullable:True
           /// </summary>
           public string AreaNumber {get;set;}
           /// <summary>
           /// Desc:行政区划名称
           /// Default:
           /// Nullable:True
           /// </summary>
           public string AreaName {get;set;}
           /// <summary>
           /// Desc:上级行政区域编号
           /// Default:
           /// Nullable:True
           /// </summary>
           public string SuperiorAreaNumber {get;set;}
           /// <summary>
           /// Desc:行政区划级别 
           /// Default:
           /// Nullable:True
           /// </summary>
           public string AreaLv {get;set;}
           /// <summary>
           /// Desc:拼音码
           /// Default:
           /// Nullable:True
           /// </summary>
           public string PYM {get;set;}
           /// <summary>
           /// Desc:五笔码
           /// Default:
           /// Nullable:True
           /// </summary>
           public string WBM {get;set;}

    }
}
