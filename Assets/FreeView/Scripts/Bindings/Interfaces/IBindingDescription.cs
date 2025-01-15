using System;

namespace FreeView.Bindings.Interfaces
{
    public interface IBindingDescription : IDisposable
    {
        void ApplySet();
    }
}