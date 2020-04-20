using System;

namespace JQ.Common.Model
{
    public interface IEntity
    {
        int ID { get; set; }

        int IsDel { get; set; }

        string Update_By { get; set; }

        DateTime? Update_Time { get; set; }

        string Create_By { get; set; }

        DateTime? Create_Time { get; set; }

        int IsUpload { get; set; }

        DateTime? Upload_Time { get; set; }
    }
}