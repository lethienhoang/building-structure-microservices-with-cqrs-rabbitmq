using App.Services.Account.Contract.Responses;
using Framework.Types;
using System;

namespace App.Services.Account.Queries
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
