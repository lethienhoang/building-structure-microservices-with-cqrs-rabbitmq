using System.Threading.Tasks;
using App.Services.Identity.Contract.Requests;
using App.Services.Identity.Messages.Commands;
using App.Services.Identity.Queries;
using Framework.Auth;
using Framework.CQRS.Dispatchers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Services.Identity.Controllers
{
    [Route("api/identity")]
    [ApiController]
    public class IdentityController : ApiControllerBase
    {
        private readonly IDispatcher _dispatcher;
        public IdentityController(IClaimService claimService,
                                  IDispatcher dispatcher) : base(claimService)
        {
            _dispatcher = dispatcher;
        }

        [HttpPost]
        [Route("sign-up")]
        [AllowAnonymous]
        public async Task<ActionResult> SignUp([FromBody] SignUpRequest request)
        {
            var command = new SignUpCommand(request.Email, request.Password);
            await _dispatcher.SendAsync(command);

            return Ok();
        }

        [HttpPost]
        [Route("sign-in")]
        [AllowAnonymous]
        public async Task<ActionResult> SignIn([FromBody] SignInRequest request)
        {
            var query = new SignInQuery(request.Email, request.Password);
            var result = await _dispatcher.QueryAsync(query);

            return Ok(result);
        }

        [HttpPost]
        [Route("me/password")]
        public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            var userContext = _claimService.GetUserContext();

            var command = new ChangedPasswordCommand(userContext.Claims.OnUserId, request.NewPassword);
            await _dispatcher.SendAsync(command);

            return Ok();
        }
    }
}