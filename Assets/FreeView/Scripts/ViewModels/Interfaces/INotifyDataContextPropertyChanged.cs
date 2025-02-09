using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FreeView.Scripts.ViewModels.Interfaces
{
    public delegate void DataContextPropertyChangedEventHandler(object sender, DataContextPropertyChangedEventArgs e);

    public interface INotifyDataContextPropertyChanged : INotifyPropertyChanged
    {
        event DataContextPropertyChangedEventHandler DataContextPropertyChanged;
        void NotifyPropertyChanged<T>(string name, T value);
        bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string? propertyName = null);
    }
}