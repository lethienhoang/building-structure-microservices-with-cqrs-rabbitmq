using App.Framework.Identity;
using Microsoft.AspNetCore.Mvc;

namespace App.Services.Identity.Controllers
{
    [Route("api/[controller")]
    [ApiController]
    public class ApiControllerBase : ControllerBase
    {
        protected readonly IClaimService _claimService;

        public ApiControllerBase(IClaimService claimService)
        {
            _claimService = claimService;
        }
    }
}