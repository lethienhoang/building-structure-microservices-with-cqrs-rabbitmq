using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.Services.Identity.Controllers
{
    [Route("tokens")]
    [ApiController]
    public class TokensController : ApiControllerBase
    {
        public TokensController(IClaimService claimService) : base(claimService)
        {
        }
    }
}