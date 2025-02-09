using System;

namespace FreeView.Scripts.Views.Interfaces
{
    public interface IViewLoader
    {
        BaseView LoadView(Type viewModelType);
    }
}