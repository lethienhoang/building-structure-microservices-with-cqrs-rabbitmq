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
    [Route("api/tokens")]
    [ApiController]
    public class TokensController : ApiControllerBase
    {
        private readonly IDispatcher _dispatcher;
        public TokensController(IClaimService claimService, IDispatcher dispatcher) : base(claimService)
        {
            _dispatcher = dispatcher;
        }

        [HttpPost("access-tokens/{refreshToken}/refresh")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshAccessToken(string refreshToken)
        {
            var query = new AccessTokenQuery(refreshToken);
            var result = await _dispatcher.QueryAsync(query);

            return Ok(result);
        }

        [HttpPost("access-tokens/revoke")]
        public async Task<IActionResult> RevokeAccessToken([FromBody] RevokeTokenRequest request)
        {
            var command = new RevokeAccessTokenCommand(request.UserId, request.Token);
            await _dispatcher.SendAsync(command);

            return NoContent();
        }

        [HttpPost("refresh-tokens/revoke")]
        public async Task<IActionResult> RevokeRefreshToken([FromBody] RevokeTokenRequest request)
        {
            var command = new RevokeRefreshTokenCommand(request.Token, request.UserId);
            await _dispatcher.SendAsync(command);

            return NoContent();
        }
    }
}