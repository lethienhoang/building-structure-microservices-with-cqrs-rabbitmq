using Framework.Auth;
using Framework.Types;

namespace App.Services.Identity.Queries
{
    public class SignInQuery : IQuery<JsonWebToken>
    {
        public string Email { get; private set; }

        public string Password { get; private set; }

        protected SignInQuery() { }

        public SignInQuery(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
