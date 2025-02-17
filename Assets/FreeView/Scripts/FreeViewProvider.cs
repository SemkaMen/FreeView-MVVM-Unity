using System;
using System.Collections.Generic;
using FreeView.Scripts.ViewModels;
using FreeView.Scripts.ViewModels.Interfaces;
using FreeView.Scripts.Views;
using FreeView.Scripts.Views.Interfaces;

namespace FreeView.Scripts
{
    public class FreeViewProvider
    {
        private IViewsContainer _viewsContainer;
        private IViewModelProvider _viewModelProvider;
        private IViewModelLocator _viewModelLocator;
        private IViewLoader _viewLoader;
        private IViewPresenter _viewPresenter;

        protected IViewsContainer ViewsContainer => _viewsContainer ??= InstantiateViewContainer();
        protected IViewModelProvider ViewModelProvider => _viewModelProvider ??= InstantiateViewModelProvider();
        protected IViewModelLocator ViewModelLocator => _viewModelLocator ??= InstantiateViewModelLocator();
        protected IViewLoader ViewLoader => _viewLoader ??= InstantiateViewLoader();
        protected IViewPresenter ViewPresenter => _viewPresenter ??= InstantiateViewPresenter();
        
        public FreeViewProvider(Dictionary<Type, Type> viewsMapping)
        {
            MapViewsFromSelector(viewsMapping);
        }
        
        public FreeViewProvider(IViewsTemplateSelector viewsTemplateSelector)
        {
            MapViewsFromSelector(viewsTemplateSelector?.ViewMapping);
        }

        public void Hide<TViewModel>() where TViewModel : IBaseViewModel
        {
            ViewPresenter?.Hide<TViewModel>();
        }

        public void Show<TViewModel>() where TViewModel : IBaseViewModel
        {
            ViewPresenter?.Show<TViewModel>();
        }

        public void Show<TViewModel, TNavigationArgs>(TNavigationArgs navigationArgs)
            where TViewModel : IBaseViewModel<TNavigationArgs>
        {
            ViewPresenter?.Show<TViewModel, TNavigationArgs>(navigationArgs);
        }

        protected virtual IViewPresenter InstantiateViewPresenter()
        {
            return new ViewPresenter(ViewLoader, ViewModelLocator, ViewsContainer);
        }

        protected virtual IViewModelProvider InstantiateViewModelProvider()
        {
            return new ViewModelProvider();
        }

        protected virtual IViewModelLocator InstantiateViewModelLocator()
        {
            return new ViewModelLocator(ViewModelProvider);
        }

        protected virtual IViewsContainer InstantiateViewContainer()
        {
            return new ViewsContainer();
        }

        protected virtual IViewLoader InstantiateViewLoader()
        {
            return new ViewLoader(ViewsContainer);
        }
        
        private void MapViewsFromSelector(Dictionary<Type, Type> viewMapping)
        {
            foreach (var map in viewMapping)
                ViewsContainer.Add(map.Key, map.Value);
        }
    }
}