using Core.MVVM.Bindings.Interfaces;

namespace Core.MVVM.Bindings
{
    public class BindingContext : IBindingContext
    {
        public object DataContext { get; set; }
    }
}