using Microsoft.AspNetCore.Mvc;

namespace RateLimit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase  
    {
        [HttpGet]
        public IActionResult GetCompany()
        {
            return Ok(new { Id = 1, Name = "Tesla", Date = DateTime.Now.ToString() });
        }

        [HttpPost]
        public IActionResult SaveCompany()
        {
            return Ok(new { Id = 1, CompanyName = "Renault", Date = DateTime.Now.ToString() });
        }
    }
}
