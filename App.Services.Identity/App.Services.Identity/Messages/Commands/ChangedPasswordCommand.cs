using Framework.CQRS.Messages;
using Framework.Types;
using Newtonsoft.Json;
using System;

namespace App.Services.Identity.Messages.Commands
{

    [MessageNamespace("identityservice")]
    public class ChangedPasswordCommand : ICommand
    {
        public Guid UserId { get; private set; }

        public string PasswordHash { get; private set; }

        protected ChangedPasswordCommand()
        {

        }

        [JsonConstructor]
        public ChangedPasswordCommand(Guid userId, string passwordHash)
        {
            UserId = userId;
            PasswordHash = passwordHash;
        }
    }
}
