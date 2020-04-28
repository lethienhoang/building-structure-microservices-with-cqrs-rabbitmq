using Autofac;
using Framework.CQRS.Handlers;
using Framework.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using RawRabbit;
using RawRabbit.Common;
using RawRabbit.Configuration;
using RawRabbit.Enrichers.MessageContext;
using RawRabbit.Instantiation;
using System;
using System.Reflection;

namespace Framework.RabbitMq
{
    public static class Extensions
    {
        private static string SectionName = "RabbitMq";
        public static T GetService<T>(this IServiceProvider provider)
        {
            return (T)provider.GetService(typeof(T));
        }

        public static IBusSubscriber UseRabbitMq(this IApplicationBuilder app)
            => new BusSubscriber(app);

        public static void AddRabbitMq(this ContainerBuilder builder)
        {
            builder.Register(context =>
            {
                var configuration = context.Resolve<IConfiguration>();
                var options = configuration.GetOptions<RabbitMqOptions>(SectionName);
                return options;
            })
             .SingleInstance();

            builder.Register(context =>
            {
                var configuration = context.Resolve<IConfiguration>();
                var options = configuration.GetOptions<RawRabbitConfiguration>(SectionName);
                return options;
            })
             .SingleInstance();

            var assembly = Assembly.GetCallingAssembly();
            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(IEventHandler<>))
                .InstancePerDependency();
            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(ICommandHandler<>))
                .InstancePerDependency();
            builder.RegisterType<BusPublisher>().As<IBusPublisher>()
                .InstancePerDependency();

            ConfigureBus(builder);
        }

        private static void ConfigureBus(ContainerBuilder builder)
        {
            builder.Register<IInstanceFactory>(context =>
            {
                var options = context.Resolve<RabbitMqOptions>();
                var configuration = context.Resolve<RawRabbitConfiguration>();
                var namingConventions = new CustomNamingConventions(options.Namespace);

                return RawRabbitFactory.CreateInstanceFactory(new RawRabbitOptions
                {
                    DependencyInjection = ioc =>
                    {
                        ioc.AddSingleton(options);
                        ioc.AddSingleton(configuration);
                        ioc.AddSingleton<INamingConventions>(namingConventions);

                    },
                    Plugins = p => p
                        .UseAttributeRouting()
                        .UseRetryLater()
                        .UseMessageContext<CorrelationContext>()
                        .UseContextForwarding()
                });
            }).SingleInstance();

            builder.Register(context => context.Resolve<IInstanceFactory>().Create());
        }

        private class CustomNamingConventions : NamingConventions
        {
            public CustomNamingConventions(string defaultNamespace)
            {
                ExchangeNamingConvention = type => (type.GetCustomAttribute<MessageNamespaceAttribute>()?.Namespace ??
                     defaultNamespace).ToLowerInvariant();
                RoutingKeyConvention = type =>
                    $"#.{type.GetCustomAttribute<MessageNamespaceAttribute>()?.Namespace ?? defaultNamespace}.{type.Name.Underscore()}"
                    .ToLowerInvariant();
            }
        }
    }
}
