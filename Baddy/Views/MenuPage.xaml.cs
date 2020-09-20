using Baddy.ViewModels;
using CommonServiceLocator;
using Xamarin.Forms;
using MenuItem = Baddy.Models.MenuItem;

namespace Baddy.Views
{
    public partial class MenuPage : ContentPage
    {
        public MenuPage()
        {
            InitializeComponent();

            BindingContext = ServiceLocator.Current.GetInstance<MenuViewModel>();
        }

        private void ListViewMenu_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            ((MenuItem)e.SelectedItem).Handler();
        }
    }
}