using FreeView.Bindings.Interfaces;
using FreeView.ViewModels.Interfaces;

namespace FreeView.Views.Interfaces
{
    public interface IBaseView : IBindable
    {
        void SetVisible(bool isVisible);

        IBaseViewModel ViewModel { get; set; }
    }
}