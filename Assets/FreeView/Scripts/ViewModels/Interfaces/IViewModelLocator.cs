using System;

namespace FreeView.ViewModels.Interfaces
{
    public interface IViewModelLocator
    {
        IBaseViewModel Load(Type viewModelType);
        IBaseViewModel Load<TViewModel>() where TViewModel : IBaseViewModel;
        IBaseViewModel<TNavigationArgs> Load<TViewModel, TNavigationArgs>(TNavigationArgs args)
            where TViewModel : IBaseViewModel<TNavigationArgs>;
    }
}