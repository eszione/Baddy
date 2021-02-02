using System.Threading.Tasks;

namespace Baddy.Interfaces
{
    public interface INavigationService
    {
        Task CloseMenu();
        Task NavigateTo<T>(params object[] parameters);
        Task ShowPopup<T>(bool isToast = false, params object[] parameters);
        Task NavigateToHome();
        Task RefreshMenu();
    }
}
