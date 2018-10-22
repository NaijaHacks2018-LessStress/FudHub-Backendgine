using FudHub.Data.Models;
using FudHub.Engine.Logics;
using FudHub.WebAPI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace FudHub.WebAPI.Controllers
{
    [RoutePrefix("api/category")]
    public class CategoryController : BaseApiController
    {
        [HttpGet]
        [Route("load/{categoryID?}")]
        public HttpResponseMessage Load(int? categoryID = null)
        {
            try
            {
                var r = new ApiResult<object>();

                if (categoryID.HasValue)
                {
                    var rsp = new CategoryManager().Get(categoryID.Value);
                    r.ResponseCode = ResponseCode.Success;
                    r.ResponseMessage = (rsp != null) ? "Categories has been loaded" : "No category found";
                    r.Data = rsp;
                }
                else
                {
                    var rsp = new CategoryManager().Load();
                    r.ResponseCode = ResponseCode.Success;
                    r.ResponseMessage = (rsp.Count > 0) ? "Categories has been loaded" : "No category found";
                    r.Data = rsp;
                }

                return Request.CreateResponse(r);
            }
            catch (Exception ex)
            {
                return CreateErrorResponse(ex);
            }
        }

        [HttpPost]
        [Route("add")]
        public HttpResponseMessage Add(Category category)
        {
            try
            {
                var rsp = new CategoryManager().Add(category);
                var r = new ApiResult<bool>
                {
                    ResponseCode = ResponseCode.Success,
                    ResponseMessage = rsp.Item2,
                    Data = rsp.Item1
                };
                return Request.CreateResponse(r);
            }
            catch (Exception ex)
            {
                return CreateErrorResponse(ex);
            }
        }
    }
}
