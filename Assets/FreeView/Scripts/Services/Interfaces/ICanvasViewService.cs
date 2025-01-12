using System.Collections.Generic;
using Core.MVVM.Views.interfaces;
using FreeView.Scripts.ViewModels.Interfaces;

namespace Core.MVVM.Services.Interfaces
{
    public interface ICanvasViewService
    {
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