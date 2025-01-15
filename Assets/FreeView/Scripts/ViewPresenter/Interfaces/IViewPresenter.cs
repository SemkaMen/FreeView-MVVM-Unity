using FreeView.ViewModels.Interfaces;
using FreeView.Views;

namespace FreeView.ViewPresenter.Interfaces
{
    public interface IViewPresenter
    {
        bool CanPresent<TViewModel>() where TViewModel : IBaseViewModel;
        BaseView PresentView<TViewModel>() where TViewModel : IBaseViewModel;
        BaseView PresentView<TViewModel, TNavigationArgs>(TNavigationArgs navigationArgs) where TViewModel : IBaseViewModel<TNavigationArgs>;
    }
}