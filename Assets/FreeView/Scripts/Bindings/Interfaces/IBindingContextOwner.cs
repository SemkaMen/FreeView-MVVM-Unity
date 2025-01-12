namespace Core.MVVM.Bindings.Interfaces
{
    public interface IBindingContextOwner
    {
        IBindingContext BindingContext { get; set; }
    }
}