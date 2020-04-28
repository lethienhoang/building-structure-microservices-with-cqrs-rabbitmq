using Framework.CQRS.Messages;
using Framework.Types;
using Newtonsoft.Json;
using System;

namespace App.Services.Identity.Messages.Commands
{
    public class SignUpCommand : ICommand
    {
        public Guid Id { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }

        protected SignUpCommand() { }

        [JsonConstructor]
        public SignUpCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
