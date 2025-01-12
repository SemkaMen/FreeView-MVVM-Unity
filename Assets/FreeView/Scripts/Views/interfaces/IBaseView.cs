using Core.MVVM.Bindings.Interfaces;
using FreeView.Scripts.ViewModels.Interfaces;

namespace Core.MVVM.Views.interfaces
{
    public interface IBaseView : IBindable
    {
        void SetVisible(bool isVisible);

        IBaseViewModel ViewModel { get; set; }
    }
}