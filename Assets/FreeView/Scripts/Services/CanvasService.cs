using System;
using System.Collections.Generic;
using System.Linq;
using FreeView.Services.Interfaces;
using FreeView.ViewModels.Interfaces;
using FreeView.ViewPresenter.Interfaces;
using FreeView.Views.Interfaces;

namespace FreeView.Services
{
    public class CanvasService : ICanvasService, IDisposable
    {
        private readonly IViewPresenter _viewPresenter;
        private readonly IViewsContainer _viewsContainer;
        private readonly List<IBaseView> _childViews;
        private bool _disposed;
        
        public CanvasService(IViewPresenter viewPresenter, IViewsContainer viewsContainer)
        {
            _viewPresenter = viewPresenter;
            _viewsContainer = viewsContainer;
            _childViews = new List<IBaseView>();
        }

        public void Hide<TViewModel>() where TViewModel : IBaseViewModel
        {
            SetVisibility<TViewModel>(false);
        }
        
        public void Show<TViewModel>() where TViewModel : IBaseViewModel
        {
            InitView<TViewModel>();
            SetVisibility<TViewModel>(true);
        }

        public void Show<TViewModel, TNavigationArgs>(TNavigationArgs navigationArgs)
            where TViewModel : IBaseViewModel<TNavigationArgs>
        {
            InitView<TViewModel, TNavigationArgs>(navigationArgs);
            SetVisibility<TViewModel>(true);
        }
        
        public void Dispose()
        {
            if (_disposed)
                return;

            foreach (var view in _childViews.OfType<IDisposable>())
            {
                view.Dispose();
            }

            _childViews.Clear();
            _disposed = true;
        }
        
        private void AddView(IBaseView view)
        {
            if (view == null) throw new ArgumentNullException(nameof(view));
            if (!_childViews.Contains(view)) _childViews.Add(view);
        }

        private void SetVisibility<TViewModel>(bool isVisible) where TViewModel : IBaseViewModel
        {
            FindViewByViewModel<TViewModel>()?.SetVisible(isVisible);
        }
        
        private IBaseView FindViewByViewModel<TViewModel>()
        {
            return _childViews.FirstOrDefault(v => v?.ViewModel?.GetType() == typeof(TViewModel));
        }

        private void InitView<TViewModel>() where TViewModel : IBaseViewModel
        {
            var view = _childViews.FirstOrDefault(v => v.ViewModel is TViewModel);
            if (view == null)
            {
                view = _viewPresenter.PresentView<TViewModel>();
                AddView(view);
            }
        }

        private void InitView<TViewModel, TNavigationArgs>(TNavigationArgs navigationArgs)
            where TViewModel : IBaseViewModel<TNavigationArgs>
        {
            var view = _childViews.FirstOrDefault(v => v.ViewModel is TViewModel);
            if (view == null)
            {
                view = _viewPresenter.PresentView<TViewModel, TNavigationArgs>(navigationArgs);
                AddView(view);
            }
        }
    }
}
