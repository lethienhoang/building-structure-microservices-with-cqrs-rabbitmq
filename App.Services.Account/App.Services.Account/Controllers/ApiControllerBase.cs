using Framework.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Services.Account.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ApiControllerBase : ControllerBase
    {
        protected readonly IClaimService _claimService;

        public ApiControllerBase(IClaimService claimService)
        {
            _claimService = claimService;
        }
    }
}