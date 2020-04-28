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
    public class ChangePasswordHandler : ICommandHandler<ChangePasswordCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IBusPublisher _busPublisher;
        public ChangePasswordHandler(IUserRepository userRepository,
                                     IPasswordHasher<User> passwordHasher,
                                     IBusPublisher busPublisher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _busPublisher = busPublisher;
        }

        public async Task HandleAsync(ChangePasswordCommand command, ICorrelationContext context)
        {
            var user = await _userRepository.GetAsync(command.UserId);

            if (user == null)
            {
                throw new DomainException(Codes.UserNotFound, string.Format(MessagesCode.UserNotFound, command.UserId));
            }

            if (!user.ValidatePassword(command.PasswordHash, _passwordHasher))
            {
                throw new DomainException(Codes.InvalidCurrentPassword, MessagesCode.InvalidCurrentPassword);
            }

            user.SetPassword(command.PasswordHash, _passwordHasher);

            await _busPublisher.SendAsync(new ChangePasswordCommand(user.Id, user.PasswordHash), CorrelationContext.Empty);
        }
    }
}
