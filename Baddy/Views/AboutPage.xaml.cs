using Baddy.ViewModels;
using CommonServiceLocator;
using System.ComponentModel;
using Xamarin.Forms;

namespace Baddy.Views
{
    [DesignTimeVisible(false)]
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();

            BindingContext = ServiceLocator.Current.GetInstance<AboutViewModel>();
        }
    }
}