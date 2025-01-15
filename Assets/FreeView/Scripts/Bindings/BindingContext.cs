using FreeView.Bindings.Interfaces;

namespace FreeView.Bindings
{
    public class BindingContext : IBindingContext
    {
        public object DataContext { get; set; }
    }
}