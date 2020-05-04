using App.Services.Account.Messages.Commands;
using App.Services.Account.Repositories;
using Framework.CQRS.Handlers;
using Framework.Types;
using System.Threading.Tasks;

namespace App.Services.Account.Handlers
{
    public class ChangedPasswordHandler : ICommandHandler<ChangedPasswordCommand>
    {
        private readonly IUserRepository _userRepository;
        public ChangedPasswordHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task HandleAsync(ChangedPasswordCommand command, ICorrelationContext context)
        {
            await _userRepository.ChangePasswordAsync(command.UserId, command.PasswordHash);
        }
    }
}
