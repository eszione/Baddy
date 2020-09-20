using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Baddy.Styles
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConverterStyles : ResourceDictionary
    {
        public static ConverterStyles Instance => new ConverterStyles();

        public ConverterStyles()
        {
            InitializeComponent();
        }
    }
}