using Framework.CQRS.Messages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Services.Account.Messages.Commands
{
    public class ChangePasswordCommand : ICommand
    {
        public Guid UserId { get; private set; }

        public string PasswordHash { get; private set; }

        protected ChangePasswordCommand() { }

        [JsonConstructor]
        public ChangePasswordCommand(Guid userId, string passwordHash)
        {
            UserId = userId;
            PasswordHash = passwordHash;
        }
    }
}
