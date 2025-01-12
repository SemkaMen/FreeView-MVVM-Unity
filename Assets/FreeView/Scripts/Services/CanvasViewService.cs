using System.Collections.Generic;
using System.Linq;
using Core.MVVM.Services.Interfaces;
using Core.MVVM.Views.interfaces;
using FreeView.Scripts.ViewModels.Interfaces;

namespace Core.MVVM.Services
{
    public class CanvasViewService : ICanvasViewService
    {
        public List<IBaseView> ChildViews { get; } = new();
        
        public void AddView(IBaseView view, bool isVisible = true)
        {
            if (ChildViews.Contains(view)) return;
            ChildViews.Add(view);
            view.SetVisible(isVisible);
        }

        public void RemoveView<TViewModel>()
        {
            var vmType = typeof(TViewModel);
            var view = ChildViews.FirstOrDefault(v => v?.ViewModel?.GetType() == vmType);
            if (view != null) ChildViews.Remove(view);
        }

        public void Clear()
        {
            ChildViews?.Clear();
        }

        public void SetVisibility<TViewModel>(bool isVisible) where TViewModel : IBaseViewModel
        {
            var vmType = typeof(TViewModel);
            var view = ChildViews.FirstOrDefault(v => v?.ViewModel?.GetType() == vmType);
            if (view == null) return;
            view.ViewModel.Prepare();
            view.SetVisible(isVisible);
        }
        
        public void SetVisibility<TViewModel, TNavigationArgs>(bool isVisible, TNavigationArgs navigationArgs)
            where TViewModel : IBaseViewModel<TNavigationArgs>  
        {
            var view = ChildViews.FirstOrDefault(v =>
            {
                var vmType = typeof(TViewModel);
                var vvmType = v.ViewModel?.GetType();
                return vvmType == vmType;
            });
            if (view == null) return;
            if (view.ViewModel is IBaseViewModel<TNavigationArgs> navArgsViewModel)
                navArgsViewModel.Prepare(navigationArgs);
            view.SetVisible(isVisible);
        }

        public void Hide<TViewModel>(TViewModel viewModel) where TViewModel : IBaseViewModel
        {
            SetVisibility<TViewModel>(false);
        }

        public void Show<TViewModel>(TViewModel viewModel) where TViewModel : IBaseViewModel
        {
            SetVisibility<TViewModel>(true);
        }
    }
}