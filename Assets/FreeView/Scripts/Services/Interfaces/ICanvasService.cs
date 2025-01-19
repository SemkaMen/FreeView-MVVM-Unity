using FreeView.ViewModels.Interfaces;

namespace FreeView.Services.Interfaces
{
    public interface ICanvasService
    {
        void Hide<TViewModel>(TViewModel viewModel) where TViewModel : IBaseViewModel;
        void Show<TViewModel>(TViewModel viewModel) where TViewModel : IBaseViewModel;
        void Show<TViewModel, TNavigationArgs>(TViewModel viewModel, TNavigationArgs navigationArgs)
            where TViewModel : IBaseViewModel<TNavigationArgs>;
    }
}