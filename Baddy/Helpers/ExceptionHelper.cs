using Baddy.Models;
using System.Net;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Baddy.Helpers
{
    public class ExceptionHelper
    {
        public static async Task HandleInvasive(HttpException ex)
        {
            switch (ex.Code)
            {
                case HttpStatusCode.BadRequest:
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
                        break;
                    }
                case HttpStatusCode.Unauthorized:
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "Unauthorized access!", "Ok");
                        break;
                    }
                default:
                    break;
            }
        }

        public static string Handle(HttpException ex)
        {
            switch (ex.Code)
            {
                case HttpStatusCode.BadRequest:
                    return ex.Message;
                case HttpStatusCode.Unauthorized:
                    return "Unauthorized access!";
                default:
                    return "Unknown error";
            }
        }
    }
}
