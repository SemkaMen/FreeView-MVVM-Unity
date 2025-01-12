using System;
using System.Collections.Generic;
using System.Reflection;
using Core.MVVM.Views;
using Core.MVVM.Views.Attributes;
using Core.MVVM.Views.interfaces;
using FreeView.Scripts.ViewModels.Interfaces;
using UnityEngine;

namespace FreeView.Scripts.Views
{
    public class ViewLoader : IViewLoader
    {
        private readonly Dictionary<Type, Type> _bindingMap = new();
        private const string Path = "Prefabs/Views/";
        
        private string GetPrefabPath(string prefabName)
        {
            return $"{Path}{prefabName}";
        }

        public Type GetViewType(Type? viewModelType)
        {
            if (viewModelType != null && _bindingMap.TryGetValue(viewModelType, out var binding))
                return binding;
            
            throw new KeyNotFoundException("Could not find view for " + viewModelType);
        }

        public void Initialize()
        {
            CreateMapping();
        }

        public BaseView LoadView(Type viewModelType)
        {
            BaseView viewInstance = null;
            var viewType = GetViewType(viewModelType);
            var prefabName = string.Empty;
                
            if (viewType.GetCustomAttribute<ViewPresentationAttribute>() is { } viewPresentationAttribute)
                prefabName = viewPresentationAttribute.PrefabPath;
            
            if (string.IsNullOrEmpty(prefabName))
                prefabName = viewType.Name;
                
            if (!string.IsNullOrEmpty(prefabName)) 
                viewInstance = Resources.Load<BaseView>(GetPrefabPath(prefabName));

            return viewInstance;
        }

        private void CreateMapping()
        {
        }
        
        private void Add<TViewModel, TView>()
            where TViewModel : IBaseViewModel
            where TView : IBaseView
        {
            Add(typeof(TViewModel), typeof(TView));
        }

        private void Add(Type viewModelType, Type viewType)
        {
            _bindingMap[viewModelType] = viewType;
        }
    }
}