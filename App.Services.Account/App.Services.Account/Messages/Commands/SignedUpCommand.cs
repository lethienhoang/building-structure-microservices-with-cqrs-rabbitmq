using Framework.CQRS.Messages;
using Newtonsoft.Json;
using System;

namespace App.Services.Account.Messages.Commands
{
    public class SignedUpCommand : ICommand
    {
        public Guid Id { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }

        protected SignedUpCommand() { }

        [JsonConstructor]
        public SignedUpCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
