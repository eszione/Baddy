using Xamarin.Forms;
using Baddy.Views;
using Baddy.Styles;
using Baddy.Interfaces;

namespace Baddy
{
    public partial class App : Application
    {
        public App(IAppContext appContext)
        {
            InitializeComponent();

            SetStyles();

            MainPage = new MainPage(appContext);
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
            dictionary.Add(ConverterStyles.Instance);
        }
    }
}
