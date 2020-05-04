using Framework.CQRS.Messages;
using System;

namespace App.Services.Identity.Messages.Commands
{
    public class RevokeAccessTokenCommand : ICommand
    {
        public Guid UserId { get; private set; }

        public string Token { get; private set; }

        protected RevokeAccessTokenCommand() { }

        public RevokeAccessTokenCommand(Guid userId, string token)
        {
            UserId = userId;
            Token = token;
        }
    }
}
