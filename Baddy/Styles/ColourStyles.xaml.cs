using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Baddy.Styles
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ColourStyles : ResourceDictionary
    {
        public static ColourStyles Instance => new ColourStyles();

        public ColourStyles()
        {
            InitializeComponent();
        }
    }
}