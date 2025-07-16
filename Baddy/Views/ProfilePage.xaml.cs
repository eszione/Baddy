using Baddy.ViewModels;
using CommonServiceLocator;
using Xamarin.Forms;

namespace Baddy.Views
{
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();

            BindingContext = ServiceLocator.Current.GetInstance<ProfileViewModel>();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await ((ProfileViewModel)BindingContext).SetProfile();
        }
    }
}
