using Baddy.Interfaces;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Baddy.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel(
            IAppContext appContext,
            INavigationService navigationService) : base(appContext, navigationService)
        {
            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://xamarin.com"));
        }

        public ICommand OpenWebCommand { get; }
    }
}