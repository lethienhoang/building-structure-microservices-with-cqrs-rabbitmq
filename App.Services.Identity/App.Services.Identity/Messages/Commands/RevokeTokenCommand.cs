using Framework.CQRS.Messages;
using Newtonsoft.Json;
using System;

namespace App.Services.Identity.Messages.Commands
{
    public class RevokeTokenCommand : ICommand
    {
        public string Token { get; }

        public Guid UserId { get; }

        [JsonConstructor]
        public RevokeTokenCommand(string token, Guid userId)
        {
            Token = token;
            UserId = userId;
        }
    }
}
