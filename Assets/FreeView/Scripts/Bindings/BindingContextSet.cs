using System;
using System.Collections.Generic;
using Core.MVVM.Bindings.Interfaces;

namespace Core.MVVM.Bindings
{
    public class BindingContextSet<TTarget, TSource> : IBindingContextSet where TTarget : class, IBindingContextOwner
    {
        private readonly TTarget _target;
        private readonly TSource _dataContext;
        private readonly List<IBindingDescription> _bindingSets = new ();

        public BindingContextSet(TTarget owner)
        {
            _target = owner;
            _dataContext = (TSource)owner.BindingContext.DataContext;
        }
        
        public void Apply()
        {
            foreach (var set in _bindingSets)
                set.ApplySet();
        }

        public BindingDescription<TTarget, TSource> Bind()
        {
            var bindingDescription = new BindingDescription<TTarget, TSource>(_target, _dataContext);
            _bindingSets.Add(bindingDescription);
            return bindingDescription;
        }
        
        public BindingDescription<TChildTarget, TSource> Bind<TChildTarget>(TChildTarget childTarget)
            where TChildTarget : class, IBindable
        {
            var bindingDescription = new BindingDescription<TChildTarget, TSource>(childTarget, _dataContext);
            _bindingSets.Add(bindingDescription);
            return bindingDescription;
        }

        public void Dispose()
        {
            foreach (var set in _bindingSets) set.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}