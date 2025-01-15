using System;

namespace FreeView.Views.Interfaces
{
    public interface IViewLoader
    {
        BaseView LoadView(Type viewModelType);
    }
}