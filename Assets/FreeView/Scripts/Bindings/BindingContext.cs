using FreeView.Scripts.Bindings.Interfaces;

namespace FreeView.Scripts.Bindings
{
    public class BindingContext : IBindingContext
    {
        public object DataContext { get; set; }
    }
}