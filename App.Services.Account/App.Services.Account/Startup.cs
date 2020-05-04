using System.Linq;
using System.Reflection;
using App.Services.Account.Domain;
using App.Services.Account.Messages.Commands;
using Autofac;
using Framework;
using Framework.Auth;
using Framework.CQRS.Dispatchers;
using Framework.HTTP;
using Framework.Middlewares;
using Framework.MongoDb;
using Framework.RabbitMq;
using Framework.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace App.Services.Account
{
    public class Startup
    {
        private static readonly string[] Headers = new[] { "X-Operation", "X-Resource", "X-Total-Count" };
        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerDocs();
            services.AddJwt();
            services.AddInitializers(typeof(IMongoDbInitializer));
            services.AddHealthChecks();
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
            //services.RegisterNamedHttpClientFactories();
            services.AddHttpClient();
            services.AddTransient<IClaimService, ClaimService>();
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                    builder.SetIsOriginAllowed(_ => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .SetIsOriginAllowed((host) => true)
                    .WithExposedHeaders(Headers));
            });

            services.AddOptions();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.AddDispatchers();

            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                .Where(x => x.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.AddRabbitMq();
            builder.AddMongoDb();
            builder.AddMongoRepository<User>("Users");
            builder.AddMongoRepository<Role>("Roles");
            builder.AddMongoRepository<UserRoles>("UserRoles");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IStartupInitializer startupInitializer)
        {
            if (env.IsDevelopment() || env.EnvironmentName == "local")
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");

            app.UseSwaggerDocs();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseApiExceptionHandler();

            app.UseRabbitMq()
                .SubscribeCommand<SignedUpCommand>(_namespace: "identityservice")
                .SubscribeCommand<ChangedPasswordCommand>(_namespace: "identityservice");

            app.UseAuthentication();

            startupInitializer.InitializeAsync();

            app.UseMvc();
        }
    }
}
