using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RateLimit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetCustomer()
        {
            return Ok(new { Id = 1, Name = "Ford Otosan", Date = DateTime.Now.ToString() });
        }
    }
}
