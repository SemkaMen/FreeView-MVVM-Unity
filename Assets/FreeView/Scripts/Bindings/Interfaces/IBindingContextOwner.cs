namespace FreeView.Bindings.Interfaces
{
    public interface IBindingContextOwner
    {
        IBindingContext BindingContext { get; set; }
    }
}