using System;
using FreeView.Scripts.ViewModels.Interfaces;

namespace Core.MVVM.ViewModelLocator.Interfaces
{
    public interface IViewModelProvider
    {
        IBaseViewModel ResolveViewModel<TViewModel>() where TViewModel : IBaseViewModel;
        IBaseViewModel ResolveViewModel(Type viewModelType);
        IBaseViewModel<TNavigationArgs> ResolveViewModel<TViewModel, TNavigationArgs>(TNavigationArgs args)
            where TViewModel : IBaseViewModel<TNavigationArgs>;
        IBaseViewModel<TNavigationArgs> ResolveViewModel<TNavigationArgs>(Type viewModelType, TNavigationArgs args);
    }
}