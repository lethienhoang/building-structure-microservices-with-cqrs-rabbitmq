using Framework.CQRS.Messages;
using Framework.Types;
using Newtonsoft.Json;
using System;

namespace App.Services.Identity.Messages.Commands
{

    [MessageNamespace("identityservice")]
    public class ChangePasswordCommand : ICommand
    {
        public Guid UserId { get; }

        public string PasswordHash { get; }

        [JsonConstructor]
        public ChangePasswordCommand(Guid userId, string passwordHash)
        {
            UserId = userId;
            PasswordHash = passwordHash;
        }
    }
}
