using App.Services.Identity.Infrastructure.Constants;
using Framework.Domain;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace App.Services.Identity.Domain
{
    public class User : EntityBase
    {
        private static readonly Regex EmailRegex = new Regex(
            @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
            RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

        public string Email { get; private set; }

        public IList<UserRoles> UserRoles { get; private set; }

        public string PasswordHash { get; private set; }

        protected User() 
        {
            UserRoles = new List<UserRoles>();
        }

        public User(string email)
        {
            if (!EmailRegex.IsMatch(email))
            {
                throw new DomainException(Codes.InvalidEmail, string.Format(MessagesCode.InvalidEmail, email));
            }

            Email = email.ToLowerInvariant();
        }

        public void SetPassword(string password, IPasswordHasher<User> passwordHasher)
        {
            PasswordHash = passwordHasher.HashPassword(this, password);
        }

        public bool ValidatePassword(string password, IPasswordHasher<User> passwordHasher)
        {
            return passwordHasher.VerifyHashedPassword(this, PasswordHash, password) != PasswordVerificationResult.Failed;
        }
    }
}
