using InternetBanking.Core.Application.Dtos.Account.Response;
using InternetBanking.Core.Application.Helpers;

namespace InternetBanking.Middlewares
{
    public class ValidateUserSession
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public ValidateUserSession(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public bool HasUser()
        {
            AuthenticationResponse authentication = httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");

            if (authentication == null)
            {
                return false;
            }
            return true;
        }

    }
}
