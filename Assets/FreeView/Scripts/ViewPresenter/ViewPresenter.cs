using System;
using System.Linq;
using System.Reflection;
using FreeView.Views.Interfaces;
using FreeView.ViewModelLocator.Interfaces;
using FreeView.ViewModels.Interfaces;
using FreeView.ViewPresenter.Interfaces;
using FreeView.Views;
using FreeView.Views.Attributes;
using UnityEngine;
using Object = UnityEngine.Object;

namespace FreeView.ViewPresenter
{
    public class ViewPresenter : IViewPresenter
    {
        private readonly IViewLoader _viewLoader;
        private readonly IViewModelLocator _viewModelLocator;
        private readonly IViewsContainer _viewsContainer;

        public ViewPresenter(IViewLoader viewLoader, IViewModelLocator viewModelLocator, IViewsContainer viewsContainer)
        {
            _viewLoader = viewLoader;
            _viewModelLocator = viewModelLocator;
            _viewsContainer = viewsContainer;
        }
        
        public bool CanPresent<TViewModel>()
            where TViewModel : IBaseViewModel
        {
            return _viewsContainer.GetViewType(typeof(TViewModel)) != null;
        }
        
        public BaseView PresentView<TViewModel>() where TViewModel : IBaseViewModel
        {
            var view = GetView<TViewModel>();
            view.ViewModel ??= _viewModelLocator.Load<TViewModel>();
            return view;
        } 
        
        public BaseView PresentView<TViewModel, TNavigationArgs>(TNavigationArgs navigationArgs) where TViewModel : IBaseViewModel<TNavigationArgs>
        {
            var view = GetView<TViewModel>();
            view.ViewModel ??= _viewModelLocator.Load<TViewModel, TNavigationArgs>(navigationArgs);
            return view;
        }

        private BaseView GetView<TViewModel>() where TViewModel : IBaseViewModel
        {
            if(!CanPresent<TViewModel>())
                throw new NullReferenceException($"Can't present view for type:{typeof(TViewModel)}");

            var view = _viewLoader.LoadView(typeof(TViewModel));
            if (view == null)
                throw new NullReferenceException($"View not found when trying to show view for type: {typeof(TViewModel)}");
            
            Transform parent = null;
            if (view.GetType().GetCustomAttribute<ViewPresentationAttribute>() is { } viewPresentationAttribute)
            {
                var viewContainer = Object.FindObjectsOfType<RectTransform>().FirstOrDefault(c => c.transform.name.Equals(viewPresentationAttribute.CanvasContainerName));
                if (viewContainer == null)
                    throw new NullReferenceException($"ViewContainer not found when trying to show view for type: {typeof(TViewModel)}");

                parent = viewContainer.transform;
            }
            view.gameObject.SetActive(false);
            return Object.Instantiate(view, parent, false);
        }
    }
}