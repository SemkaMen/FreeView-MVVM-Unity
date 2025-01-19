using System;
using System.Collections.Generic;
using FreeView.ViewModelLocator.Interfaces;
using FreeView.ViewModels.Interfaces;

namespace FreeView.ViewModelLocator
{
    public class ViewModelProvider : IViewModelProvider
    {
        private Dictionary<Type, object> instances = new Dictionary<Type, object>();
        public IBaseViewModel ResolveViewModel<TViewModel>() where TViewModel : IBaseViewModel => ResolveInstance<TViewModel>();

        public IBaseViewModel ResolveViewModel(Type viewModelType) => ResolveInstance<IBaseViewModel>(viewModelType);

        private TViewModel ResolveInstance<TViewModel>(Type viewModelType = null) where TViewModel : IBaseViewModel
        {
            lock (this.instances)
            {
                var type = viewModelType ?? typeof(TViewModel) ;
                if (this.instances.TryGetValue(type, out var instance))
                {
                    return (TViewModel)instance;
                }

                var newInstance = (TViewModel)Activator.CreateInstance(type);
                this.instances.Add(type, newInstance);

                return newInstance;
            }
        }
    }
}