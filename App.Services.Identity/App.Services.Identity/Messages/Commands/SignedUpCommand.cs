using Framework.CQRS.Messages;
using Framework.Types;
using Newtonsoft.Json;

namespace App.Services.Identity.Messages.Commands
{
    [MessageNamespace("accountservice")]
    public class SignedUpCommand : ICommand
    {
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
