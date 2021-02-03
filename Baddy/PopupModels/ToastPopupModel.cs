namespace Baddy.PopupModels
{
    public class ToastPopupModel
    {
        public string Message { get; set; }
        public ToastPopupModel(string message)
        {
            Message = message;
        }
    }
}
