using System;
using FreeView.ViewModelLocator.Interfaces;
using FreeView.ViewModels.Interfaces;

namespace FreeView.ViewModelLocator
{
    public class ViewModelProvider : IViewModelProvider
    {
        public IBaseViewModel ResolveViewModel<TViewModel>() where TViewModel : IBaseViewModel
        {
            throw new NotImplementedException();
        }

        public IBaseViewModel ResolveViewModel(Type viewModelType)
        {
            throw new NotImplementedException();
        }

        public IBaseViewModel<TNavigationArgs> ResolveViewModel<TViewModel, TNavigationArgs>(TNavigationArgs args)
            where TViewModel : IBaseViewModel<TNavigationArgs>
        {
            throw new NotImplementedException();
        }

        public IBaseViewModel<TNavigationArgs> ResolveViewModel<TNavigationArgs>(Type viewModelType,
            TNavigationArgs args)
        {
            throw new NotImplementedException();
        }
    }
}