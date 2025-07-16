using Baddy.ViewModels;
using CommonServiceLocator;
using Xamarin.Forms;

namespace Baddy.Views
{
    public partial class BookingsPage : ContentPage
    {
        public BookingsPage()
        {
            InitializeComponent();

            BindingContext = ServiceLocator.Current.GetInstance<BookingsViewModel>();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await ((BookingsViewModel)BindingContext).SetBookings();
        }
    }
}
