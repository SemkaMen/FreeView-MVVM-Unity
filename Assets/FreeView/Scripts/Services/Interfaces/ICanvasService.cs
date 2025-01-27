using FreeView.ViewModels.Interfaces;

namespace FreeView.Services.Interfaces
{
    public interface ICanvasService
    {
        void Hide<TViewModel>() where TViewModel : IBaseViewModel;
        void Show<TViewModel>() where TViewModel : IBaseViewModel;
        void Show<TViewModel, TNavigationArgs>(TNavigationArgs navigationArgs)
            where TViewModel : IBaseViewModel<TNavigationArgs>;
    }
}