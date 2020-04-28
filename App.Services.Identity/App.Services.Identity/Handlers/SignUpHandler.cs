using App.Services.Identity.Domain;
using App.Services.Identity.Infrastructure.Constants;
using App.Services.Identity.Messages.Commands;
using App.Services.Identity.Repositories;
using Framework.CQRS.Handlers;
using Framework.Domain;
using Framework.RabbitMq;
using Framework.Types;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace App.Services.Identity.Handlers
{
    public class SignUpHandler : ICommandHandler<SignUpCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IBusPublisher _busPublisher;
        public SignUpHandler(IUserRepository userRepository,
                             IPasswordHasher<User> passwordHasher,
                             IBusPublisher busPublisher)
        {
            _userRepository = userRepository;
            _busPublisher = busPublisher;
            _passwordHasher = passwordHasher;
        }

        public async Task HandleAsync(SignUpCommand command, ICorrelationContext context)
        {
            var user = await _userRepository.GetAsync(command.Email);

            if (user != null)
            {
                throw new DomainException(Codes.EmailInUse, string.Format(MessagesCode.EmailInUse, command.Email));
            }

            if (string.IsNullOrWhiteSpace(command.Password))
            {
                throw new DomainException(Codes.InvalidPassword, MessagesCode.InvalidPassword);
            }

            user = new User(command.Email);

            user.SetPassword(command.Password, _passwordHasher);

            await _busPublisher.SendAsync(new SignedUpCommand(user.Email, user.PasswordHash), CorrelationContext.Empty);
        }
    }
}
