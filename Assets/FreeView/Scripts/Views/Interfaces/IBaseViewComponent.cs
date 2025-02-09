using System.ComponentModel;
using FreeView.Scripts.Bindings.Interfaces;

namespace FreeView.Scripts.Views.Interfaces
{
    public interface IBaseViewComponent : INotifyPropertyChanged, IBindable
    {
        
    }
}