using Xamarin.Forms;
using Baddy.Views;
using Baddy.Styles;

namespace Baddy
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            SetStyles();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private void SetStyles()
        {
            dictionary.Add(ColourStyles.Instance);
            dictionary.Add(CommonStyles.Instance);
        }
    }
}
