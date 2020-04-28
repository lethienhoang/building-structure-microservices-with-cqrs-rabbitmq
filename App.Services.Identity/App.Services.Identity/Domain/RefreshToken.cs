using App.Services.Identity.Infrastructure.Constants;
using Framework.Domain;
using Framework.Types;
using Microsoft.AspNetCore.Identity;
using System;

namespace App.Services.Identity.Domain
{
    public class RefreshToken : EntityBase
    {
        public Guid UserId { get; private set; }

        public string Token { get; private set; }

        public DateTime? RevokedAt { get; private set; }

        public bool Revoked => RevokedAt.HasValue;

        protected RefreshToken() { }

        public RefreshToken(User user, IPasswordHasher<User> passwordHasher)
        {
            Id = Guid.NewGuid();
            UserId = user.Id;
            CreatedAt = DateTime.UtcNow;
            Token = CreateToken(user, passwordHasher);
        }

        public void Revoke()
        {
            if (Revoked)
            {
                throw new DomainException(Codes.RefreshTokenAlreadyRevoked, string.Format(MessagesCode.RefreshTokenAlreadyRevoked,
                                                                                          Id,
                                                                                          RevokedAt));
            }

            RevokedAt = DateTimeHelper.GenerateTodayUTC();
        }

        private static string CreateToken(User user, IPasswordHasher<User> passwordHasher)
            => passwordHasher.HashPassword(user, Guid.NewGuid().ToString("N"))
                .Replace("=", string.Empty)
                .Replace("+", string.Empty)
                .Replace("/", string.Empty);
    }
}
