using App.Services.Account.Domain;
using App.Services.Account.Messages.Commands;
using App.Services.Account.Repositories;
using Framework.CQRS.Handlers;
using Framework.Types;
using System;
using System.Threading.Tasks;

namespace App.Services.Account.Handlers
{
    public class SignUpHandler : ICommandHandler<SignedUpCommand>
    {
        private readonly IUserRepository _userRepository;
        public SignUpHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task HandleAsync(SignedUpCommand command, ICorrelationContext context)
        {
            var user = new User(Guid.NewGuid(), command.Email, command.Password);

            await _userRepository.AddAsync(user);
        }
    }
}
