using Framework.Auth;
using Microsoft.AspNetCore.Mvc;

namespace App.Services.Account.Controllers
{
    [Route("account")]
    [ApiController]
    public class AccountController : ApiControllerBase
    {
        public AccountController(IClaimService claimService) : base(claimService)
        {
        }
    }
}