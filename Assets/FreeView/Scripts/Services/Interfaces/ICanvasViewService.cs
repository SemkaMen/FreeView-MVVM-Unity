using System.Collections.Generic;
using FreeView.ViewModels.Interfaces;
using FreeView.Views.Interfaces;

namespace FreeView.Services.Interfaces
{
    public interface ICanvasViewService
    {
        void InitView<TViewModel>(bool isVisible = true) where TViewModel : IBaseViewModel;
        void InitView<TViewModel, TNavigationArgs>(TNavigationArgs navigationArgs, bool isVisible = true) 
            where TViewModel : IBaseViewModel<TNavigationArgs>;
        void Hide<TViewModel>(TViewModel viewModel) where TViewModel : IBaseViewModel;
        void Show<TViewModel>(TViewModel viewModel) where TViewModel : IBaseViewModel;
        List<IBaseView> ChildViews { get; }
        void AddView(IBaseView view, bool isVisible = true);
        void RemoveView<TViewModel>();
        void Clear();
        void SetVisibility<TViewModel>(bool isVisible) where TViewModel : IBaseViewModel;
        void SetVisibility<TViewModel, TNavigationArgs>(bool isVisible, TNavigationArgs navigationArgs)
            where TViewModel : IBaseViewModel<TNavigationArgs>  ;
    }
}