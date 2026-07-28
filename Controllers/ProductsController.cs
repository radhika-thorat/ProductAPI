using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;

namespace ProductAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }  
        
        [HttpGet]
        public IActionResult Get()
        {
                return Ok("Product API V1");
        }
       
    }
}
