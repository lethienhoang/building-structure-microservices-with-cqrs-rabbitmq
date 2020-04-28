using System;

namespace App.Services.Identity.Contract.Requests
{
    public class ChangePasswordRequest
    {
        public Guid UserId { get; set; }

        public string NewPassword { get; set; }
    }
}
