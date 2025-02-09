using System;

namespace FreeView.Scripts.Bindings.Interfaces
{
    public interface IBindingDescription : IDisposable
    {
        void ApplySet();
    }
}