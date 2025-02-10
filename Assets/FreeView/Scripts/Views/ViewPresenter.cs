using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FreeView.Scripts.ViewModels.Interfaces;
using FreeView.Scripts.Views.Attributes;
using FreeView.Scripts.Views.Interfaces;
using UnityEngine;
using UnityObject = UnityEngine.Object;

namespace FreeView.Scripts.Views
{
    public class ViewPresenter : IViewPresenter, IDisposable
    {
        private readonly IViewLoader _viewLoader;
        private readonly IViewModelLocator _viewModelLocator;
        private readonly IViewsContainer _viewsContainer;
        private readonly List<IBaseView> _childViews;
        private bool _disposed;

        public ViewPresenter(IViewLoader viewLoader, IViewModelLocator viewModelLocator, IViewsContainer viewsContainer)
        {
            _viewLoader = viewLoader;
            _viewModelLocator = viewModelLocator;
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

        public virtual void Dispose()
        {
            if (_disposed)
                return;

            foreach (var view in _childViews.OfType<IDisposable>())
                view.Dispose();

            _childViews.Clear();
            _disposed = true;
        }

        private bool CanPresent<TViewModel>() where TViewModel : IBaseViewModel
        {
            return _viewsContainer.GetViewType(typeof(TViewModel)) != null;
        }

        private BaseView PresentView<TViewModel>() where TViewModel : IBaseViewModel
        {
            var view = GetView<TViewModel>();
            view.ViewModel ??= _viewModelLocator.Load<TViewModel>();
            return view;
        }

        private BaseView PresentView<TViewModel, TNavigationArgs>(TNavigationArgs navigationArgs)
            where TViewModel : IBaseViewModel<TNavigationArgs>
        {
            var view = GetView<TViewModel>();
            view.ViewModel ??= _viewModelLocator.Load<TViewModel, TNavigationArgs>(navigationArgs);
            return view;
        }

        private BaseView GetView<TViewModel>() where TViewModel : IBaseViewModel
        {
            if (!CanPresent<TViewModel>())
                throw new NullReferenceException($"Can't present view for type:{typeof(TViewModel)}");

            var view = _viewLoader.LoadView(typeof(TViewModel));
            if (view == null)
                throw new NullReferenceException(
                    $"View not found when trying to show view for type: {typeof(TViewModel)}");

            Transform parent = null;
            if (view.GetType().GetCustomAttribute<ViewPresentationAttribute>() is { } viewPresentationAttribute)
            {
                var viewContainer = UnityObject.FindObjectsOfType<RectTransform>().FirstOrDefault(c =>
                    c.transform.name.Equals(viewPresentationAttribute.CanvasContainerName));
                if (viewContainer == null)
                    throw new NullReferenceException(
                        $"ViewContainer not found when trying to show view for type: {typeof(TViewModel)}");

                parent = viewContainer.transform;
            }

            view.gameObject.SetActive(false);
            return UnityObject.Instantiate(view, parent, false);
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
                view = PresentView<TViewModel>();
                AddView(view);
            }
        }

        private void InitView<TViewModel, TNavigationArgs>(TNavigationArgs navigationArgs)
            where TViewModel : IBaseViewModel<TNavigationArgs>
        {
            var view = _childViews.FirstOrDefault(v => v.ViewModel is TViewModel);
            if (view == null)
            {
                view = PresentView<TViewModel, TNavigationArgs>(navigationArgs);
                AddView(view);
            }
        }
    }
}