using System;
using System.Reflection;
using FreeView.Views.Interfaces;
using FreeView.Views.Attributes;
using UnityEngine;

namespace FreeView.Views
{
    public class ViewLoader : IViewLoader
    {
        private const string Path = "Prefabs/Views/";
        private readonly IViewsContainer _viewsContainer;

        public ViewLoader(IViewsContainer viewsContainer)
        {
            _viewsContainer = viewsContainer;
        }

        public BaseView LoadView(Type viewModelType)
        {
            BaseView viewInstance = null;
            var viewType = _viewsContainer.GetViewType(viewModelType);
            var prefabName = string.Empty;
                
            if (viewType.GetCustomAttribute<ViewPresentationAttribute>() is { } viewPresentationAttribute)
                prefabName = viewPresentationAttribute.PrefabPath;
            
            if (string.IsNullOrEmpty(prefabName))
                prefabName = viewType.Name;
                
            if (!string.IsNullOrEmpty(prefabName))
            {
                var prefabPath = $"{Path}{prefabName}";
                viewInstance = Resources.Load<BaseView>(prefabPath);
            }

            return viewInstance;
        }
    }
}