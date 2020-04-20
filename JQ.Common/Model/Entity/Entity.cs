using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JQ.Common.Model
{
    public class Entity : IEntity
    {
        /// <summary>
        /// Desc:ID 自增，非主键
        /// Default:
        /// Nullable:False
        /// </summary>
        [Column("ID")]
        public int ID { get; set; }

        /// <summary>
        /// Desc:删除标记
        /// Default:
        /// Nullable:True
        /// </summary>
        [Column("ISDEL")]
        public int IsDel { get; set; }

        /// <summary>
        /// Desc:更新人
        /// Default:
        /// Nullable:True
        /// </summary>
        [Column("UPDATE_BY")]
        public string Update_By { get; set; }

        /// <summary>
        /// Desc:更新时间
        /// Default:
        /// Nullable:True
        /// </summary>
        [Column("UPDATE_TIME")]
        public DateTime? Update_Time { get; set; }

        /// <summary>
        /// Desc:创建人
        /// Default:
        /// Nullable:True
        /// </summary>
        [Column("CREATE_BY")]
        public string Create_By { get; set; }

        /// <summary>
        /// Desc:创建时间
        /// Default:
        /// Nullable:True
        /// </summary>
        [Column("CREATE_TIME")]
        public DateTime? Create_Time { get; set; }

        /// <summary>
        /// Desc:上传标记
        /// Default:
        /// Nullable:True
        /// </summary>
        [Column("ISUPLOAD")]
        public int IsUpload { get; set; }

        /// <summary>
        /// Desc:上传时间
        /// Default:
        /// Nullable:True
        /// </summary>
        [Column("UPLOAD_TIME")]
        public DateTime? Upload_Time { get; set; }
    }
}