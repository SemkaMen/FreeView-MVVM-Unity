using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FreeView.Services.Interfaces;
using FreeView.ViewModels.Interfaces;

namespace FreeView.ViewModels
{
    public abstract class BaseViewModel<TNavigationArgs> : BaseViewModel, IBaseViewModel<TNavigationArgs>
    {
        protected BaseViewModel(ICanvasViewService canvasViewService) : base(canvasViewService)
        {
        }

        public virtual void Prepare(TNavigationArgs args)
        {
        }
    }

    public abstract class BaseViewModel : IBaseViewModel, INotifyDataContextPropertyChanged
    {
        protected ICanvasViewService CanvasViewService { get; }
        public event PropertyChangedEventHandler PropertyChanged;

        public BaseViewModel(ICanvasViewService canvasViewService)
        {
            CanvasViewService = canvasViewService;
        }
        
        public event DataContextPropertyChangedEventHandler DataContextPropertyChanged;

        public void NotifyPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        
        public void NotifyPropertyChanged<T>(string name, T value)
        {
            DataContextPropertyChanged?.Invoke(this, new DataContextPropertyChangedEventArgs(name, value));
            NotifyPropertyChanged(name);
        }
        
        public virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value)) return false;
            storage = value;
            NotifyPropertyChanged(propertyName, value);
            return true;
        }

        protected void Show()
        {
            CanvasViewService.Show(this);
        }
    
        protected void Hide()
        {
            CanvasViewService.Hide(this);
        }

        public virtual void OnViewStart()
        {
        
        }

        public virtual void OnViewEnable()
        {
        
        }

        public virtual void OnViewDisable()
        {
        }

        public virtual void OnViewDestroy()
        {
        }

        public virtual void OnViewAwake()
        {
            
        }

        public void Prepare()
        {
        }

        public virtual void Initialize()
        {
        }

        public virtual void Start()
        {
        }
    }
}