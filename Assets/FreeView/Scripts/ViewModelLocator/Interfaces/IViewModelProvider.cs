using System;
using FreeView.ViewModels.Interfaces;

namespace FreeView.ViewModelLocator.Interfaces
{
    public interface IViewModelProvider
    {
        IBaseViewModel ResolveViewModel<TViewModel>() where TViewModel : IBaseViewModel;
        IBaseViewModel ResolveViewModel(Type viewModelType);
    }
}