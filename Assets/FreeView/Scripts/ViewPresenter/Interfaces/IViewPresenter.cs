using Core.MVVM.Views;
using FreeView.Scripts.ViewModels.Interfaces;

namespace FreeView.Scripts.ViewPresenter.Interfaces
{
    public interface IViewPresenter
    {
        bool CanPresent<TViewModel>() where TViewModel : IBaseViewModel;
        BaseView PresentView<TViewModel>() where TViewModel : IBaseViewModel;
        BaseView PresentView<TViewModel, TNavigationArgs>(TNavigationArgs navigationArgs) where TViewModel : IBaseViewModel<TNavigationArgs>;
    }
}