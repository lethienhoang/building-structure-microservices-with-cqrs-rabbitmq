
using Autofac;

namespace Framework.CQRS.Dispatchers
{
    public static class Extensions
    {
        public static void AddDispatchers(this ContainerBuilder builder)
        {
            builder.RegisterType<CommandDispatcher>().As<ICommandDispatcher>().SingleInstance();
            builder.RegisterType<Dispatcher>().As<IDispatcher>().SingleInstance();
            builder.RegisterType<QueryDispatcher>().As<IQueryDispatcher>().SingleInstance();
        }
    }
}
