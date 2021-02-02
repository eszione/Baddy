using Baddy.Interfaces;
using System.ComponentModel;
using Unity.Resolution;
using Unity;
using Rg.Plugins.Popup.Pages;
using Baddy.PopupModels;

namespace Baddy.Views
{
    [DesignTimeVisible(false)]
    public partial class ToastPopup : PopupPage
    {
        public ToastPopup(IAppContext appContext, string message)
        {
            InitializeComponent();

            BindingContext = appContext.Container.Resolve<ToastViewModel>(
                new ResolverOverride[] {
                    new ParameterOverride("message", message)
                }
            );
        }
    }
}