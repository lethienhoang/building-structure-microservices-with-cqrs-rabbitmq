using Microsoft.AspNetCore.Mvc;

namespace App.Services.Identity.Controllers
{
    [Route("api/healthcheck")]
    [ApiController]
    public class HealthcheckController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            return Ok("Identity Service is running");
        }
    }
}