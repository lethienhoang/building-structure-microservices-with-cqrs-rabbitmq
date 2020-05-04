using Framework.CQRS.Messages;

namespace App.Services.Identity.Messages.Commands
{
    public class RefreshAccessTokenCommand : ICommand
    {
        public string Token { get; private set; }

        protected RefreshAccessTokenCommand()
        {
        }

        public RefreshAccessTokenCommand(string token)
        {
            Token = token;
        }
    }
}
