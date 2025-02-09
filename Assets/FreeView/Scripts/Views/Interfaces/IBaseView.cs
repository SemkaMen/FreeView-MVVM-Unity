using FreeView.Scripts.Bindings.Interfaces;
using FreeView.Scripts.ViewModels.Interfaces;

namespace FreeView.Scripts.Views.Interfaces
{
    public interface IBaseView : IBindable
    {
        void SetVisible(bool isVisible);

        IBaseViewModel ViewModel { get; set; }
    }
}