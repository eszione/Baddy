using Baddy.Enums;
using Baddy.Interfaces;
using System.ComponentModel;
using Xamarin.Forms;

namespace Baddy.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : FlyoutPage
    {
        public MainPage(IAppContext appContext)
        {
            InitializeComponent();

            FlyoutLayoutBehavior = FlyoutLayoutBehavior.Popover;

            Detail = new NavigationPage(new HomePage(appContext, AppState.Initialize));
        }
    }
}