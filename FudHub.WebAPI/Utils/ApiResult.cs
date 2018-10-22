using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FudHub.WebAPI.Utils
{
    public enum ResponseCode
    {
        Success = 1,
        Failed = 2,
        Denied = 3
    }

    public class ApiResult<T>
    {
        public ResponseCode ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public T Data { get; set; }

        public ApiResult()
        {
            this.ResponseCode = ResponseCode.Failed;
        }
    }
}