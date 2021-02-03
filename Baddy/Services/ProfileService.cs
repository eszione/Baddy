using Baddy.Constants;
using Baddy.Interfaces;
using Baddy.Models;
using System.Threading.Tasks;
using System.Xml;

namespace Baddy.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IHttpService _httpService;

        public ProfileService(
            IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<Profile> Get()
        {
            var doc = await _httpService.GetXml(UrlConstants.Profile);

            return MapProfile(doc);
        }

        private Profile MapProfile(XmlDocument doc)
        {
            var mainDiv = doc.GetElementsByTagName("form")[0].ChildNodes[1];
            var personalInfo = mainDiv.SelectSingleNode("div//div[@id='personal-info']");
            var infoSettings = mainDiv.SelectSingleNode("div/div/div[@class='my-info-settings']");

            var profile = new Profile
            {
                AdditionalEmail = personalInfo.SelectSingleNode("div/input[@name='email1']/@value").InnerText,
                Balance = infoSettings.SelectSingleNode("div[@title='Balance']/div[contains(@class,'form-control')]").InnerText,
                CardExpiry = infoSettings.SelectSingleNode("div[@title='Card expiry']/div[contains(@class,'form-control')]").InnerText,
                CardNumber = personalInfo.SelectSingleNode("div/input[@name='card']/@value").InnerText,
                City = personalInfo.SelectSingleNode("div/input[@name='city']/@value").InnerText,
                DateOfBirth = mainDiv.SelectSingleNode("div//div[@title='Date of birth']/input[@name='dob']").InnerText,
                Email = personalInfo.SelectSingleNode("div/input[@name='email']/@value").InnerText,
                Gender = mainDiv.SelectSingleNode("div//div[@title='Gender']/div[@class='form-control']/div/label[contains(@class,'active')]").InnerText.Trim(),
                Home = personalInfo.SelectSingleNode("div/input[@name='phone_h']/@value").InnerText,
                Mobile = personalInfo.SelectSingleNode("div/input[@name='phone_c']/@value").InnerText,
                Name = personalInfo.SelectSingleNode("div/input[@name='name']/@value").InnerText,
                RefundInfo = infoSettings.SelectSingleNode("div[@title='No refund for bookings deleted within this period before the booking']/div[contains(@class,'form-control')]").InnerText,
                Street = personalInfo.SelectSingleNode("div/input[@name='street']/@value").InnerText,
                Suburb = personalInfo.SelectSingleNode("div/input[@name='suburb']/@value").InnerText,
            };

            try
            {
                var nameSplit = profile.Name.Split(' ');
                profile.FirstName = nameSplit[0];
                profile.Surname = nameSplit[1];
            }
            catch
            {
                profile.FirstName = profile.Name;
            }

            return profile;
        }
    }
}
