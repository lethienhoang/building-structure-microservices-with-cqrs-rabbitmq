using Framework.Domain;
using System;
using System.Collections.Generic;

namespace App.Services.Account.Domain
{
    public class User : EntityBase
    {
        public string Email { get; private set; }

        public IList<UserRoles> UserRoles { get; private set; }

        public string PasswordHash { get; private set; }

        protected User() { }

        public User(Guid id, string email, string password)
        { 
            Id = id;
            Email = email.ToLowerInvariant();
            PasswordHash = password;
        }

        public void UpdatePassword(string password)
        {
            PasswordHash = password;
        }
    }
}
