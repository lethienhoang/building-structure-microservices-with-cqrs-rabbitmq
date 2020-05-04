using System;

namespace App.Services.Identity.Contract.Requests
{
    public class RevokeTokenRequest
    {
        public string Token { get; set; }

        public Guid UserId { get; set; }
    }
}
