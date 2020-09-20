using Baddy.ViewModels;
using CommonServiceLocator;
using Xamarin.Forms;

namespace Baddy.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();

            BindingContext = ServiceLocator.Current.GetInstance<LoginViewModel>();
        }
    }
}