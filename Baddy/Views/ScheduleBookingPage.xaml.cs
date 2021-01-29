using Baddy.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;
using CommonServiceLocator;

namespace Baddy.Views
{
    [DesignTimeVisible(false)]
    public partial class ScheduleBookingPage : ContentPage
    {
        public ScheduleBookingPage()
        {
            InitializeComponent();

            BindingContext = ServiceLocator.Current.GetInstance<ScheduleBookingViewModel>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<object>(this, "NavigateAway", (sender) =>
            {
                ((ScheduleBookingViewModel)BindingContext).NavigateAway = true;
            });

            ((ScheduleBookingViewModel)BindingContext).SetScheduledDefaults();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Send<object>(this, "NavigateAway");
            MessagingCenter.Unsubscribe<object>(this, "NavigateAway");
        }
    }
}