using System;
using System.Linq;
using System.Reflection;
using Core.MVVM.Views;
using Core.MVVM.Views.Attributes;
using Core.MVVM.Views.interfaces;
using FreeView.Scripts.ViewModelLocator.Interfaces;
using FreeView.Scripts.ViewModels.Interfaces;
using FreeView.Scripts.ViewPresenter.Interfaces;
using UnityEngine;
using Object = UnityEngine.Object;

namespace FreeView.Scripts.ViewPresenter
{
    public class ViewPresenter : IViewPresenter
    {
        private readonly IViewLoader _viewLoader;
        private readonly IViewModelLocator _viewModelLocator;

        public ViewPresenter(IViewLoader viewLoader, IViewModelLocator viewModelLocator)
        {
            _viewLoader = viewLoader;
            _viewModelLocator = viewModelLocator;
        }
        
        public bool CanPresent<TViewModel>()
            where TViewModel : IBaseViewModel
        {
            return _viewLoader.GetViewType(typeof(TViewModel)) != null;
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
                throw new NullReferenceException($"Can't present view for {typeof(TViewModel)}");

            var view = _viewLoader.LoadView(typeof(TViewModel));
            if (view == null)
                throw new NullReferenceException($"View not found when trying to show view for viewmodel: {typeof(TViewModel)}");
            
            Transform parent = null;
            if (view.GetType().GetCustomAttribute<ViewPresentationAttribute>() is { } viewPresentationAttribute)
            {
                var viewContainer = Object.FindObjectsOfType<RectTransform>().FirstOrDefault(c => c.transform.name.Equals(viewPresentationAttribute.ParentCanvasContainer));
                if (viewContainer == null)
                    throw new NullReferenceException($"ViewContainer not found when trying to show view for viewmodel: {typeof(TViewModel)}");

                parent = viewContainer.transform;
            }

            return Object.Instantiate(view, parent, false);
        }
    }
}