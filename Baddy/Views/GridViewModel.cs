using Baddy.Interfaces;
using Baddy.ViewModels;

namespace Baddy.Views
{
    public class GridViewModel : BaseViewModel
    {
        public GridViewModel(
           IAppContext appContext,
           INavigationService navigationService,
           IStorageService storageService) : base(appContext, navigationService, storageService)
        {
        }
    }
}
