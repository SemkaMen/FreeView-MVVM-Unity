﻿using System.Collections.Generic;
using FreeView.Scripts.Bindings;
using FreeView.Scripts.Bindings.Interfaces;
using FreeView.Scripts.ViewModels.Interfaces;
using FreeView.Scripts.Views.Interfaces;
using UnityEngine;

namespace FreeView.Scripts.Views
{
    public abstract class BaseView<TViewModel> : BaseView where TViewModel : IBaseViewModel
    {
        public new TViewModel ViewModel
        {
            get => (TViewModel)base.ViewModel;
            set => base.ViewModel = value;
        }
    }
    
    [RequireComponent(typeof(RectTransform))]
    public abstract class BaseView : MonoBehaviour, IBaseView
    {
        private IBindingContext _bindingContext;
        private string _mappingStartPrefix;
        private Dictionary<string, GameObject> _map;
        
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

        public IBaseViewModel ViewModel
        {
            get => DataContext as IBaseViewModel;
            set => DataContext = value;
        }
        
        public void SetVisible(bool isVisible)
        {
            gameObject.SetActive(isVisible);
        }

        protected T GetElementComponent<T>(string elementName)
        {
            return _map[elementName].GetComponent<T>();
        }
        
        protected virtual void OnViewAwake()
        {
            CreateElementsMap();
            ViewModel?.OnViewAwake();
        }
        
        protected virtual void OnViewStart()
        {
            ViewModel?.OnViewStart();
        }
        
        protected virtual void OnViewEnable()
        {
            ViewModel?.OnViewEnable();
        }

        protected virtual void OnViewDisable()
        {
            ViewModel?.OnViewDisable();
        }
        
        protected virtual void OnViewDestroy()
        {
            ViewModel?.OnViewDestroy();
        }
        
        private void Awake()
        {
            OnViewAwake();
        }

        private void Start()
        {
            OnViewStart();
        }

        private void OnEnable()
        {
            OnViewEnable();
        }

        private void OnDisable()
        {
            OnViewDisable();
        }

        private void OnDestroy()
        {
            OnViewDestroy();
        }
        
        private void CreateElementsMap(string prefix = "_")
        {
            _mappingStartPrefix = prefix;
            _map = new Dictionary<string, GameObject>();
            InnerCreateMap(gameObject);
        }
        
        private void InnerCreateMap(GameObject go)
        {
            var count = go.transform.childCount;
            for (int i = 0; i < count; i++)
            {
                var child = go.transform.GetChild(i);

                if (child.name.StartsWith(_mappingStartPrefix))
                    _map.Add(child.name, child.gameObject);

                if (child.childCount > 0)
                    InnerCreateMap(child.gameObject);
            }
        }
    }
}