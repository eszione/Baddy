using Rg.Plugins.Popup.Services;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Baddy.PopupModels
{
    public class ConfirmationPopupModel : BasePopupModel
    {
        public event EventHandler<bool> CallbackEvent;

        public string Message { get; set; }

        public Command CancelCommand { get; set; }
        public Command ConfirmCommand { get; set; }

        public ConfirmationPopupModel(string message)
        {
            Message = message;

            CancelCommand = new Command(async () => await Cancel());
            ConfirmCommand = new Command(async () => await Confirm());
        }

        private async Task Cancel()
        {
            await PopupNavigation.Instance.PopAsync();

            CallbackEvent.Invoke(this, false);
        }

        private async Task Confirm()
        {
            await PopupNavigation.Instance.PopAsync();

            CallbackEvent.Invoke(this, true);
        }
    }
}
