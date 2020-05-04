using Framework.CQRS.Messages;
using Newtonsoft.Json;
using System;

namespace App.Services.Identity.Messages.Commands
{
    public class RevokeRefreshTokenCommand : ICommand
    {
        public string Token { get; private set; }

        public Guid UserId { get; private set; }

        protected RevokeRefreshTokenCommand()
        {
        }

        [JsonConstructor]
        public RevokeRefreshTokenCommand(string token, Guid userId)
        {
            Token = token;
            UserId = userId;
        }
    }
}
