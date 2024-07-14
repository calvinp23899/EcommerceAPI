using EcommerceAPI.Entity.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Presentation.Controllers.v2
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiversion}/test")]
    [ApiController]
    public class TestController : CustomBaseController
    {
        public TestController()
        {
        }
        [MapToApiVersion("2.0")]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("V2");
        }
    }
}
