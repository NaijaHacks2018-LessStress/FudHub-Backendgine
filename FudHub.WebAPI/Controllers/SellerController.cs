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
    [RoutePrefix("api/seller")]
    public class SellerController : BaseApiController
    {
        [HttpGet]
        [Route("getprofile/{usernameoremail}")]
        public HttpResponseMessage GetProfile(string usernameOrEmail)
        {
            try
            {
                var rsp = new SellerManager().Get(usernameOrEmail);
                var r = new ApiResult<Seller>
                {
                    ResponseCode = ResponseCode.Success,
                    ResponseMessage = (rsp != null) ? "Seller profile has been loaded" : "Seller profile not found",
                    Data = rsp
                };

                return Request.CreateResponse(r);
            }
            catch (Exception ex)
            {
                return CreateErrorResponse(ex);
            }
        }

        [HttpGet]
        [Route("getallbylocation")]
        public HttpResponseMessage Load(string state = null, string lga = null, string location = null)
        {
            try
            {
                var rsp = new SellerManager().Load(state, lga); //(state, lga, location);
                var r = new ApiResult<IEnumerable<Seller>>
                {
                    ResponseCode = ResponseCode.Success,
                    ResponseMessage = (rsp.Count() > 0) ? "Sellers has been loaded" : "No seller found",
                    Data = rsp
                };

                return Request.CreateResponse(r);
            }
            catch (Exception ex)
            {
                return CreateErrorResponse(ex);
            }
        }

        [HttpGet]
        [Route("getall")]
        public HttpResponseMessage Load(string keyword = null, int pageIndex = 1, int pageSize = 20)
        {
            try
            {
                var rsp = new SellerManager().Load(keyword, null, pageIndex, pageSize);
                var r = new ApiResult<IEnumerable<Seller>>
                {
                    ResponseCode = ResponseCode.Success,
                    ResponseMessage = (rsp.Count() > 0) ? "Sellers has been loaded" : "No seller found",
                    Data = rsp
                };

                return Request.CreateResponse(r);
            }
            catch (Exception ex)
            {
                return CreateErrorResponse(ex);
            }
        }

        [HttpPost]
        [Route("add")]
        public HttpResponseMessage Add(Seller seller)
        {
            try
            {
                var rsp = new SellerManager().Add(seller);
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
