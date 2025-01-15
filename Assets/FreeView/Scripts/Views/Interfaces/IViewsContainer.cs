using System;
using FreeView.ViewModels.Interfaces;

namespace FreeView.Views.Interfaces
{
    public interface IViewsContainer
    {
        Type GetViewType(Type viewModelType);

        void Add(Type viewModelType, Type viewType);
        
        void Add<TViewModel, TView>()
            where TViewModel : IBaseViewModel
            where TView : IBaseView;
    }
}