using System;
using System.Linq.Expressions;
using Core.MVVM.Bindings.Interfaces;
using FreeView.Scripts.ViewModels;
using FreeView.Scripts.ViewModels.Interfaces;

namespace Core.MVVM.Bindings
{
    public class BindingDescription<TTarget, TSource> : IBindingDescription where TTarget : class
    {
        private readonly TTarget _target;
        private readonly TSource _source;
        private readonly INotifyDataContextPropertyChanged _notifyPropertyChanged;
        
        private Expression<Func<TTarget, object>> _targetProperty;
        private Expression<Func<TSource, object>> _sourceProperty;

        private string _targetPropertyName;
        private string _sourcePropertyName;
        private bool _disposed;

        public BindingDescription(TTarget target, TSource source)
        {
            _target = target;
            _source = source;

            _notifyPropertyChanged = (INotifyDataContextPropertyChanged)source;
        }
        
        public void To(Expression<Func<TSource, object>> sourceProperty)
        {
            _sourceProperty = sourceProperty;
        }
        
        public BindingDescription<TTarget, TSource> For(Expression<Func<TTarget, object>> targetProperty)
        {
            _targetProperty = targetProperty;
            return this;
        }
        
        public void ApplySet()
        {
            _notifyPropertyChanged.DataContextPropertyChanged += OnNotifyPropertyChanged;
            _sourcePropertyName = _sourceProperty.Body.GetPropertyMemberName();
            _targetPropertyName = _targetProperty.Body.GetPropertyMemberName();
            
            SetValue();
        }
        
        private void OnNotifyPropertyChanged(object sender, DataContextPropertyChangedEventArgs e)
        {
            if(!string.Equals(e.PropertyName, _sourcePropertyName)) return;
            SetValue(e.Value);
        }

        private void SetValue(object value = null)
        {
            var newValue = value ?? _source.GetPropertyValue(_sourcePropertyName);
            _target.SetPropertyValue(_targetPropertyName, newValue);
        }


        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if(disposing)
            {
                try
                {
                    _notifyPropertyChanged.DataContextPropertyChanged -= OnNotifyPropertyChanged;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            _disposed = true;
        }
    }
}