using Xamarin.Forms;

namespace Baddy.PopupModels
{
    public class BasePopupModel
    {
        public Color ThemeColor { get; private set; }

        public BasePopupModel()
        {
            ThemeColor = Color.FromHex("#2296F3");
        }
    }
}
