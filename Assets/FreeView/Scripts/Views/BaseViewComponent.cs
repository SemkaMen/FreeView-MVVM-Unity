using System.ComponentModel;
using FreeView.Scripts.Bindings;
using FreeView.Scripts.Bindings.Interfaces;
using FreeView.Scripts.Views.Interfaces;
using UnityEngine;

namespace FreeView.Scripts.Views
{
    public abstract class BaseViewComponent : MonoBehaviour, IBaseViewComponent
    {
        public string Tag => GetType().ToString();
    
        private IBindingContext _bindingContext;
    
        public IBindingContext BindingContext
        {
            get => _bindingContext ??= new BindingContext();
            set => _bindingContext = value;
        }   
    
        public object DataContext
        {
            get => BindingContext.DataContext;
            set => BindingContext.DataContext = value;
        }

        public object ViewModel
        {
            get => DataContext;
            set => DataContext = value;
        }

        public bool IsVisible
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}