using System.Collections.Generic;
using System.Threading.Tasks;
using Baddy.Constants;
using Baddy.Interfaces;
using Baddy.Models;
using Baddy.Models.Apis;

namespace Baddy.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHttpService _httpService;
        private readonly IAppContext _appContext;

        public AuthService(
            IHttpService httpService,
            IAppContext appContext)
        {
            _httpService = httpService;
            _appContext = appContext;
        }

        public async Task<LoginResult> Login(string username, string password)
        {
            var parameters = new[]
            {
                new KeyValuePair<string, string>("function", "login"),
                new KeyValuePair<string, string>("user", username),
                new KeyValuePair<string, string>("pass", password)
            };

            return await _httpService.Post<LoginResult>(parameters, UrlConstants.Main);
        }

        public async Task<bool> Authorize(string token)
        {
            var parameters = new[]
            {
                new KeyValuePair<string, string>("token", token)
            };

            return await _httpService.PostXml(parameters, UrlConstants.Authorization);
        }

        public void Logoff()
        {
            _appContext.LoggedIn = false;
            _appContext.Profile = null;
        }
    }
}
