namespace Baddy.PopupModels
{
    public class ToastViewModel
    {
        public string Message { get; set; }
        public ToastViewModel(string message)
        {
            Message = message;
        }
    }
}
