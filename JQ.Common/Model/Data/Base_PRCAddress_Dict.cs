using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace JQ.Common.Model
{
    ///<summary>
    ///中华人民共和国行政区划代码
    ///</summary>
    [Table("BASE_PRCADDRESS_DICT")]
    public partial class Base_PRCAddress_Dict:Entity
    {
           public Base_PRCAddress_Dict(){


           }
           /// <summary>
           /// Desc:机构ID
           /// Default:
           /// Nullable:True
           /// </summary>
           [Column("ORG_CODE")]
           public string Org_Code {get;set;}

           /// <summary>
           /// Desc:行政区划标准代码
           /// Default:
           /// Nullable:True
           /// </summary>
           [Column("AREASTANDARDCODE")]
           public string AreaStandardCode {get;set;}

           /// <summary>
           /// Desc:行政区划标准名称
           /// Default:
           /// Nullable:True
           /// </summary>
           [Column("AREASTANDARDNAME")]
           public string AreaStandardName {get;set;}

           /// <summary>
           /// Desc:行政区划编号
           /// Default:
           /// Nullable:True
           /// </summary>
           [Column("AREANUMBER")]
           public string AreaNumber {get;set;}

           /// <summary>
           /// Desc:行政区划名称
           /// Default:
           /// Nullable:True
           /// </summary>
           [Column("AREANAME")]
           public string AreaName {get;set;}

           /// <summary>
           /// Desc:上级行政区域编号
           /// Default:
           /// Nullable:True
           /// </summary>
           [Column("SUPERIORAREANUMBER")]
           public string SuperiorAreaNumber {get;set;}

           /// <summary>
           /// Desc:行政区划级别 自行定义，标准内没有的，方便系统操作
           /// Default:
           /// Nullable:True
           /// </summary>
           [Column("AREALV")]
           public string AreaLv {get;set;}

           /// <summary>
           /// Desc:拼音码
           /// Default:
           /// Nullable:True
           /// </summary>
           [Column("PYM")]
           public string PYM {get;set;}

           /// <summary>
           /// Desc:五笔码
           /// Default:
           /// Nullable:True
           /// </summary>
           [Column("WBM")]
           public string WBM {get;set;}


    }
}
