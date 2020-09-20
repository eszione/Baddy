using Baddy.Constants;
using Baddy.Interfaces;
using Baddy.Models;
using System.Threading.Tasks;

namespace Baddy.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IHttpService _httpService;

        public ProfileService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public Task<Profile> Get()
        {
            await _httpService.Get(UrlConstants.Profile);
        }
    }
}
