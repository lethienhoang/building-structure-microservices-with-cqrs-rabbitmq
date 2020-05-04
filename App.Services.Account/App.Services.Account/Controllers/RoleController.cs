using System.Threading.Tasks;
using App.Services.Account.Contract.Requests;
using Framework.Auth;
using Microsoft.AspNetCore.Mvc;

namespace App.Services.Account.Controllers
{
    [Route("api/role")]
    [ApiController]
    public class RoleController : ApiControllerBase
    {
        public RoleController(IClaimService claimService) : base(claimService)
        {
        }

        [HttpPost]
        [Route("assign-user-to-roles")]
        public async Task<ActionResult> AssignUserToRoles([FromBody] AssignUserToRolesRequest request)
        {           
            return Ok();
        }


        [HttpGet]
        [Route("")]
        public async Task<ActionResult> GetRoles()
        {
            return Ok();
        }
    }
}