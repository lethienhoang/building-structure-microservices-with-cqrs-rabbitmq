using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Framework
{
    public static class Extensions
    {
        public static TModel GetOptions<TModel>(this IConfiguration configuration, string section) where TModel : new()
        {
            var model = new TModel();
            configuration.GetSection(section).Bind(model);

            return model;
        }

        public static bool IsIntegerType(this Type type)
        {
            type = Nullable.GetUnderlyingType(type) ?? type;
            if (type == typeof(long)
                || type == typeof(ulong)
                || type == typeof(int)
                || type == typeof(uint)
                || type == typeof(short)
                || type == typeof(ushort)
                || type == typeof(byte)
                || type == typeof(sbyte)
                || type == typeof(System.Numerics.BigInteger))
                return true;
            return false;
        }

        public static IServiceCollection AddInitializers(this IServiceCollection services, params Type[] initializers)
           => initializers == null
               ? services
               : services.AddTransient<IStartupInitializer, StartupInitializer>(c =>
               {
                   var startupInitializer = new StartupInitializer();
                   var validInitializers = initializers.Where(t => typeof(IInitializer).IsAssignableFrom(t));
                   foreach (var initializer in validInitializers)
                   {
                       startupInitializer.AddInitializer(c.GetService(initializer) as IInitializer);
                   }

                   return startupInitializer;
               });

        public static string Underscore(this string value)
            => string.Concat(value.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString()));
    }
}
