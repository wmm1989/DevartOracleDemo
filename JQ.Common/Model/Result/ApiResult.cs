using System;
using System.Collections.Generic;
using System.Text;
using JQ.Common.Helpers;

namespace JQ.Common.Model
{


    public class ApiResult
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public static ApiResult GetResult20x<T>(T t) 
        {
            return new ApiResult()
            {
                Code = ((int)EnumResult200.Ok).ToString(),
                Message = EnumHelper.GetEnumDescription(EnumResult200.Ok),
                Data = t
            };
        }

    }

}
