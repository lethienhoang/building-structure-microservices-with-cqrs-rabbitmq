using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Framework.Auth;
using Framework.CQRS.Dispatchers;
using Framework.Swagger;
using Framework.RabbitMq;
using Autofac;
using System.Reflection;
using Framework.MongoDb;
using Framework.Middlewares;
using App.Services.Identity.Domain;
using Framework;
using Microsoft.AspNetCore.Identity;
using App.Services.Identity.Messages.Commands;
using Framework.HTTP;
using App.Services.Identity.ExternalServicesPort;
using App.Services.Identity.Repositories;
using Framework.Types;
using Framework.CQRS.Handlers;
using Framework.CQRS.Messages;

namespace App.Services.Identity
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
            services.RegisterNamedHttpClientFactories();
            services.AddHttpClient();
            services.AddTransient<IAccountService, AccountService>();
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
            builder.AddMongoRepository<RefreshToken>("RefreshTokens");
            builder.AddMongoRepository<User>("Users");
            builder.AddMongoRepository<Role>("Roles");
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

            app.UseRabbitMq();

            app.UseAuthentication();

            startupInitializer.InitializeAsync();

            app.UseMvc();
        }
    }
}
