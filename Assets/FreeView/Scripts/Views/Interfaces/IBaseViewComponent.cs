using System.ComponentModel;
using FreeView.Bindings.Interfaces;

namespace FreeView.Views.Interfaces
{
    public interface IBaseViewComponent : INotifyPropertyChanged, IBindable
    {
        
    }
}