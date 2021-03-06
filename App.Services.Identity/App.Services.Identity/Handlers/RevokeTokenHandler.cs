﻿using App.Services.Identity.Infrastructure.Constants;
using App.Services.Identity.Messages.Commands;
using App.Services.Identity.Repositories;
using Framework.CQRS.Handlers;
using Framework.Domain;
using Framework.Types;
using System.Threading.Tasks;

namespace App.Services.Identity.Handlers
{
    public class RevokeTokenHandler : ICommandHandler<RevokeTokenCommand>
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        public RevokeTokenHandler(IRefreshTokenRepository refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task HandleAsync(RevokeTokenCommand command, ICorrelationContext context)
        {
            var refreshToken = await _refreshTokenRepository.GetAsync(command.Token);

            if (refreshToken == null || refreshToken.UserId != command.UserId)
            {
                throw new DomainException(Codes.RefreshTokenNotFound, MessagesCode.RefreshTokenNotFound);
            }

            refreshToken.Revoke();
            await _refreshTokenRepository.UpdateAsync(refreshToken);
        }
    }
}
