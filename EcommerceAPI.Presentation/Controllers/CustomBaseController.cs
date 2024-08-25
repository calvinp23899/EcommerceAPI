using EcommerceAPI.Entity.ResponseModels;
using EcommerceAPI.Interface.IService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Presentation.Controllers
{
    public class CustomBaseController : ControllerBase
    {
        protected string GetRoute()
        {
            var route = Request.Path.Value;
            return route;
        }

        protected IActionResult CustomJsonResponse(int statusCode, string message)
        {
            var response = new ResponseDetails
            {
                StatusCode = statusCode,
                Message = message            
            };

            return new JsonResult(response)
            {
                StatusCode = statusCode
            };
        }
    }
}
