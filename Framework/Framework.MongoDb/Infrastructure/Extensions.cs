using Autofac;
using Framework.Domain;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Framework.MongoDb
{
    public static class Extensions
    {
        private static string SectionName = "Mongo";
        public static void AddMongoDb(this ContainerBuilder builder)
        {
            builder.Register(context =>
            {
                var configuration = context.Resolve<IConfiguration>();
                var options = configuration.GetOptions<MongoDbOptions>(SectionName);

                return options;
            })
             .SingleInstance();

            builder.Register(context =>
            {
                var options = context.Resolve<MongoDbOptions>();

                return new MongoClient(options.ConnectionString);
            })
             .SingleInstance();

            builder.Register(context =>
            {
                var options = context.Resolve<MongoDbOptions>();
                var client = context.Resolve<MongoClient>();

                return client.GetDatabase(options.Database);
            })
             .InstancePerLifetimeScope();

            builder.RegisterType<MongoDbInitializer>()
                .As<IMongoDbInitializer>()
                .InstancePerLifetimeScope();

            builder.RegisterType<MongoDbSeeder>()
                .As<IMongoDbSeeder>()
                .InstancePerLifetimeScope();
        }

        public static void AddMongoRepository<TEntity>(this ContainerBuilder builder, string collectionName) where TEntity : EntityBase, IIdentifiable
            => builder.Register(ctx => new MongoRepository<TEntity>(ctx.Resolve<IMongoDatabase>(), collectionName))
            .As<IMongoRepository<TEntity>>()
            .InstancePerLifetimeScope();
    }
}
