using Microsoft.AspNetCore.Mvc;

namespace App.Services.Account.Controllers
{
    [Route("api/healthcheck")]
    [ApiController]
    public class HealthcheckController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            return Ok("Account Service is running");
        }
    }
}