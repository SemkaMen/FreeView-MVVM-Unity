using FreeView.Scripts.ViewModels.Interfaces;

namespace FreeView.Scripts.Views.Interfaces
{
    public interface IViewPresenter
    {
        void Hide<TViewModel>() where TViewModel : IBaseViewModel;
        void Show<TViewModel>() where TViewModel : IBaseViewModel;
        void Show<TViewModel, TNavigationArgs>(TNavigationArgs navigationArgs)
            where TViewModel : IBaseViewModel<TNavigationArgs>;
        bool CanPresent<TViewModel>() where TViewModel : IBaseViewModel;
        BaseView PresentView<TViewModel>() where TViewModel : IBaseViewModel;
        BaseView PresentView<TViewModel, TNavigationArgs>(TNavigationArgs navigationArgs) where TViewModel : IBaseViewModel<TNavigationArgs>;
    }
}