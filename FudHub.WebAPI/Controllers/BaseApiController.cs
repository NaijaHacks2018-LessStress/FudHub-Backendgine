using FudHub.WebAPI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FudHub.WebAPI.Controllers
{
    public class BaseApiController : ApiController
    {
        public HttpResponseMessage CreateErrorResponse(Exception ex)
        {
            ApiResult<string> result = new ApiResult<string>();
            result.ResponseCode = 0;
            //LogError(ex.Message);

#if DEBUG
            result.ResponseMessage = ex.Message;
            return Request.CreateResponse(HttpStatusCode.InternalServerError, result);
#else
             result.ResponseMessage = "An error occurred while processing your request!";
             return Request.CreateResponse(HttpStatusCode.InternalServerError, result);
#endif
        }
    }
}