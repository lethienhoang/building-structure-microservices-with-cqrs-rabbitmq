using System.Collections.Generic;

namespace Framework.Auth
{
    public interface IJwtHandler
    {
        JsonWebToken CreateToken(string userId, IList<string> roles = null, IDictionary<string, string> claims = null);

        JsonWebTokenPayload GetTokenPayload(string accessToken);
    }
}
