using System;
using System.Collections.Generic;
using FreeView.ViewModels.Interfaces;
using FreeView.Views.Interfaces;

namespace FreeView.Views
{
    public class ViewsContainer : IViewsContainer
    {
        private readonly Dictionary<Type, Type> _bindingMap = new();
        
        public Type GetViewType(Type viewModelType)
        {
            if (viewModelType != null && _bindingMap.TryGetValue(viewModelType, out var binding))
                return binding;
            
            throw new KeyNotFoundException("Could not find view for " + viewModelType);
        }
        
        public void Add<TViewModel, TView>()
            where TViewModel : IBaseViewModel
            where TView : IBaseView
        {
            Add(typeof(TViewModel), typeof(TView));
        }

        public void Add(Type viewModelType, Type viewType)
        {
            _bindingMap[viewModelType] = viewType;
        }
    }
}