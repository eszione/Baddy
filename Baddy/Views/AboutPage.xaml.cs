using Baddy.Enums;
using Baddy.Interfaces;
using Baddy.ViewModels;
using System.ComponentModel;
using Unity.Resolution;
using Xamarin.Forms;
using Unity;

namespace Baddy.Views
{
    [DesignTimeVisible(false)]
    public partial class AboutPage : ContentPage
    {
        public AboutPage(IAppContext appContext, AppState appState = AppState.Navigate)
        {
            InitializeComponent();

            BindingContext = appContext.Container.Resolve<AboutViewModel>(
                new ResolverOverride[] {
                    new ParameterOverride("appState", appState)
                }
            );
        }
    }
}