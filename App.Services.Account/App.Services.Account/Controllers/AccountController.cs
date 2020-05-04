using App.Services.Account.Queries;
using Framework.Auth;
using Framework.CQRS.Dispatchers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace App.Services.Account.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ApiControllerBase
    {
        private readonly IDispatcher _dispatcher;
        public AccountController(IClaimService claimService, IDispatcher dispatcher) : base(claimService)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet]
        [Route("me")]
        public async Task<ActionResult> GetUserProfile()
        {
            var userContext = _claimService.GetUserContext();

            var query = new UserProfileQuery(userContext.Claims.OnUserId);

            return Ok(await _dispatcher.QueryAsync(query));
        }
    }
}