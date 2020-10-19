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
    public partial class CreateBookingPage : ContentPage
    {
        public CreateBookingPage(IAppContext appContext, AppState appState = AppState.Navigate)
        {
            InitializeComponent();

            BindingContext = appContext.Container.Resolve<CreateBookingViewModel>(
                new ResolverOverride[] {
                    new ParameterOverride("appState", appState)
                }
            );
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<object>(this, "NavigateAway", (sender) =>
            {
                ((CreateBookingViewModel)BindingContext).NavigateAway = true;
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Send<object>(this, "NavigateAway");
            MessagingCenter.Unsubscribe<object>(this, "NavigateAway");
        }
    }
}