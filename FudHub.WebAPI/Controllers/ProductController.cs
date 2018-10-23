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
    [RoutePrefix("api/product")]
    public class ProductController : BaseApiController
    {
        [HttpGet]
        [Route("getproduct/{id}")]
        public HttpResponseMessage GetProduct(int id)
        {
            try
            {
                var rsp = new ProductManager().Get(id);
                var r = new ApiResult<Product>
                {
                    ResponseCode = ResponseCode.Success,
                    ResponseMessage = (rsp != null) ? "Product details has been loaded" : "Product not found",
                    Data = rsp
                };

                return Request.CreateResponse(r);
            }
            catch (Exception ex)
            {
                return CreateErrorResponse(ex);
            }
        }

        //[HttpGet]
        //[Route("getallbylocation")]
        //public HttpResponseMessage Load(string state = null, string lga = null, string location = null)
        //{
        //    try
        //    {
        //        var rsp = new SellerManager().Load(state, lga); //(state, lga, location);
        //        var r = new ApiResult<IEnumerable<Seller>>
        //        {
        //            ResponseCode = ResponseCode.Success,
        //            ResponseMessage = (rsp.Count() > 0) ? "Sellers has been loaded" : "No seller found",
        //            Data = rsp
        //        };

        //        return Request.CreateResponse(r);
        //    }
        //    catch (Exception ex)
        //    {
        //        return CreateErrorResponse(ex);
        //    }
        //}
        
        [HttpGet]
        [Route("getall")]
        public HttpResponseMessage Load(string keyword = null, int pageIndex = 1, int pageSize = 20)
        {
            try
            {
                AppUtility.Log($"Get all products [{keyword},{pageIndex},{pageSize}]", nameof(ProductController), nameof(Load));
                var rsp = new ProductManager().Load(keyword, null, pageIndex, pageSize).OrderByDescending(p => p.ID);
                var r = new ApiResult<IEnumerable<Product>>
                {
                    ResponseCode = ResponseCode.Success,
                    ResponseMessage = (rsp.Count() > 0) ? "Products has been loaded" : "No product found",
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
        public HttpResponseMessage Add(Product seller)
        {
            try
            {
                var rsp = new ProductManager().Add(seller);
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
