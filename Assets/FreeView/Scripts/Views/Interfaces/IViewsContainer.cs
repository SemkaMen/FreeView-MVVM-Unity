using System;
using FreeView.Scripts.ViewModels.Interfaces;

namespace FreeView.Scripts.Views.Interfaces
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