using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace JQ.Common.Model
{
    ///<summary>
    ///
    ///</summary>
    [Table("BASE_FEE_ITEM_CLASS")]
    public partial class Base_Fee_Item_Class:Entity
    {
           public Base_Fee_Item_Class(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>
           [Column("ORG_CODE")]
           public string Org_Code {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>
           [Column("TREEID")]
           public string TreeID {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>
           [Column("TREEPID")]
           public string TreePID {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>
           [Column("INCOMEPROJECTCLASSID")]
           public string IncomeProjectClassID {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>
           [Column("INCOMEPROJECTCLASSNAME")]
           public string IncomeProjectClassName {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>
           [Column("PYM")]
           public string PYM {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>
           [Column("WBM")]
           public string WBM {get;set;}


    }
}
