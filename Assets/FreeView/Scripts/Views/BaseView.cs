using System.Collections.Generic;
using FreeView.Bindings;
using FreeView.Bindings.Interfaces;
using FreeView.Views.Interfaces;
using FreeView.ViewModels.Interfaces;
using UnityEngine;

namespace FreeView.Views
{
    public abstract class BaseView<TViewModel> : BaseView where TViewModel : IBaseViewModel
    {
        public new TViewModel ViewModel
        {
            get => (TViewModel)base.ViewModel;
            set => base.ViewModel = value;
        }
    }
    
    public abstract class BaseView : MonoBehaviour, IBaseView
    {
        private IBindingContext _bindingContext;
        private string _mappingStartPrefix;
        private Dictionary<string, GameObject> _map;

        [SerializeField]
        private string _label;

        public string Label => _label;
        
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
        
        protected GameObject GetElement(string elementName)
        {
            return _map[elementName];
        }

        protected T GetElementComponent<T>(string elementName)
        {
            return _map[elementName].GetComponent<T>();
        }

        protected RectTransform GetRectTransform(string name)
        {
            return _map[name].GetComponent<RectTransform>();
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