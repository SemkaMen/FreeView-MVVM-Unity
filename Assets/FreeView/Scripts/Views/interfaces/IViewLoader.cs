using System;

namespace Core.MVVM.Views.interfaces
{
    public interface IViewLoader
    {
        void Initialize();
        
        BaseView LoadView(Type viewModelType);
        
        Type GetViewType(Type viewModelType);
    }
}