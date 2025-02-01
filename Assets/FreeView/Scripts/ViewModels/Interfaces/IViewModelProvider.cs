using System;

namespace FreeView.ViewModels.Interfaces
{
    public interface IViewModelProvider
    {
        IBaseViewModel ResolveViewModel<TViewModel>() where TViewModel : IBaseViewModel;
        IBaseViewModel ResolveViewModel(Type viewModelType);
    }
}