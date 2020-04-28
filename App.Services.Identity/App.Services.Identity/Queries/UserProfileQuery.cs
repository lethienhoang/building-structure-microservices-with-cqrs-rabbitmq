using App.Services.Identity.Contract.Responses;
using Framework.Types;
using System;

namespace App.Services.Identity.Queries
{
    public class UserProfileQuery : IQuery<UserProfileResponse>
    {
        public Guid UserId { get; private set; }

        protected UserProfileQuery() { }

        public UserProfileQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}
