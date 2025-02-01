using FreeView.ViewModels.Interfaces;

namespace FreeView.Views
{
    public interface IViewPresenter
    {
        bool CanPresent<TViewModel>() where TViewModel : IBaseViewModel;
        BaseView PresentView<TViewModel>() where TViewModel : IBaseViewModel;
        BaseView PresentView<TViewModel, TNavigationArgs>(TNavigationArgs navigationArgs) where TViewModel : IBaseViewModel<TNavigationArgs>;
    }
}