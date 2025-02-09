using System;
using System.Collections.Generic;
using FreeView.Scripts.ViewModels.Interfaces;

namespace FreeView.Scripts.ViewModels
{
    public class ViewModelProvider : IViewModelProvider
    {
        private readonly Dictionary<Type, object> _instances = new();
        
        public IBaseViewModel ResolveViewModel<TViewModel>() where TViewModel : IBaseViewModel => ResolveInstance<TViewModel>();

        public IBaseViewModel ResolveViewModel(Type viewModelType) => ResolveInstance<IBaseViewModel>(viewModelType);

        private TViewModel ResolveInstance<TViewModel>(Type viewModelType = null) where TViewModel : IBaseViewModel
        {
            lock (_instances)
            {
                var type = viewModelType ?? typeof(TViewModel);
                if (_instances.TryGetValue(type, out var instance))
                    return (TViewModel)instance;

                var newInstance = (TViewModel)Activator.CreateInstance(type);
                _instances.Add(type, newInstance);

                return newInstance;
            }
        }
    }
}