using FudHub.Data.Models;
using FudHub.Data.ViewModels;
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
    [RoutePrefix("api/order")]
    public class OrderController : BaseApiController
    {
        [HttpGet]
        [Route("cancel/{id}")]
        public HttpResponseMessage Cancel(int id)
        {
            try
            {
                var rsp = new OrderManager().Cancel(id);
                var r = new ApiResult<bool>
                {
                    ResponseCode = ResponseCode.Success,
                    ResponseMessage = rsp ? $"Order #{id} has been cancelled" : "Could not cancel order",
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
        [Route("getorder/{id}")]
        public HttpResponseMessage GetOrder(int id)
        {
            try
            {
                var rsp = new OrderManager().Get(id);
                var r = new ApiResult<OrderData>
                {
                    ResponseCode = ResponseCode.Success,
                    ResponseMessage = (rsp.Item1 != null) ? "Order details has been loaded" : "Order not found",
                    Data = rsp.Item1
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
                var rsp = new OrderManager().Load(keyword, null, pageIndex, pageSize);
                var r = new ApiResult<IEnumerable<Order>>
                {
                    ResponseCode = ResponseCode.Success,
                    ResponseMessage = (rsp.Count() > 0) ? "Order has been loaded" : "No order found",
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
        public HttpResponseMessage Add(Cart cart)
        {
            try
            {
                var rsp = new OrderManager().Add(cart);
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
