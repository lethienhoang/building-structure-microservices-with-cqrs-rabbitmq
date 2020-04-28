﻿using System;
using System.Threading.Tasks;
using Autofac;
using Framework.CQRS.Handlers;
using Framework.CQRS.Messages;
using Framework.Types;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.CQRS.Dispatchers
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IComponentContext _context;

        public CommandDispatcher(IComponentContext context)
        {
            _context = context;
        }

        public async Task SendAsync<T>(T command) where T : ICommand
        {
            await _context.Resolve<ICommandHandler<T>>().HandleAsync(command, CorrelationContext.Empty);
        }
    }
}
