using System;

namespace Core.MVVM.Bindings.Interfaces
{
    public interface IBindingDescription : IDisposable
    {
        void ApplySet();
    }
}