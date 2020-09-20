using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Baddy.Styles
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CommonStyles : ResourceDictionary
    {
        public static CommonStyles Instance => new CommonStyles();

        public CommonStyles()
        {
            InitializeComponent();
        }
    }
}