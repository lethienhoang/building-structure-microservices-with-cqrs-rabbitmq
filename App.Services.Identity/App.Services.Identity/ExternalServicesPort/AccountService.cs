using System;
using System.Threading.Tasks;
using App.Services.Identity.Contract.Responses.ExternalResponses;
using Framework.HTTP;
using Framework.HTTP.Infrastructure;

namespace App.Services.Identity.ExternalServicesPort
{
    public class AccountService : IAccountService
    {
        private readonly IHttpClient _httpClient;
        public AccountService(IHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResult<UserProfileExternalResponse>> GetUserProfileAsync(Guid userId)
        {
            return await _httpClient.GetResultAsync<UserProfileExternalResponse>("AccountService", $"me/{userId}");
        }
    }
}
