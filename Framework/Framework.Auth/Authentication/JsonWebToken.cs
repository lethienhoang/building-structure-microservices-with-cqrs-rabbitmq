using System.Collections.Generic;

namespace Framework.Auth
{
    public class JsonWebToken
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public long Expires { get; set; }

        public string Id { get; set; }

        public IList<string> Roles { get; set; }

        public IDictionary<string, string> Claims { get; set; }
    }
}
