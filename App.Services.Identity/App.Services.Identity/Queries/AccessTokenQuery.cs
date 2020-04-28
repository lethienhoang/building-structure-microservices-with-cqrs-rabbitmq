using Framework.Auth;
using Framework.Types;

namespace App.Services.Identity.Queries
{
    public class AccessTokenQuery : IQuery<JsonWebToken>
    {
        public string Token { get; }

        public AccessTokenQuery(string token)
        {
            Token = token;
        }
    }
}
