using App.Services.Identity.Contract.Responses.ExternalResponses;
using Framework.HTTP;
using System;
using System.Threading.Tasks;

namespace App.Services.Identity.ExternalServicesPort
{
    public interface IAccountService
    {
        Task<HttpResult<UserProfileExternalResponse>> GetUserProfileAsync(Guid userId);
    }
}
