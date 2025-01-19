using FreeView.ViewModelLocator;
using FreeView.ViewModelLocator.Interfaces;
using FreeView.Views;
using FreeView.Views.Interfaces;

namespace FreeView.Scripts
{
    /// <summary>
    /// Entry point class for initialization
    /// </summary>
    public abstract class FreeViewSetup : IFreeViewSetup
    {
        protected IViewsContainer ViewsContainer { get; private set; }
        protected IViewModelProvider ViewModelProvider { get; private set; }
        protected IViewModelLocator ViewModelLocator { get; private set; }
        protected IViewLoader ViewLoader { get; private set; }

        public void Initialize()
        {
            ViewModelProvider = InstantiateViewModelProvider();
            ViewsContainer = InstantiateViewContainer();
            ViewModelLocator = InstantiateViewModelLocator();
            ViewLoader = InstantiateViewLoader();
            
            OnCreateViewsMap(ViewsContainer);
        }

        public virtual IViewModelProvider InstantiateViewModelProvider()
        {
            return new ViewModelProvider();
        }

        public IViewModelLocator InstantiateViewModelLocator()
        {
            return new ViewModelLocator.ViewModelLocator(ViewModelProvider);
        }

        public IViewsContainer InstantiateViewContainer()
        {
            return new ViewsContainer();
        }
        
        public virtual IViewLoader InstantiateViewLoader()
        {
            return new ViewLoader(ViewsContainer);
        }

        public abstract void OnCreateViewsMap(IViewsContainer viewsContainer);
    }

    public interface IFreeViewSetup
    {
        IViewModelProvider InstantiateViewModelProvider();
        IViewModelLocator InstantiateViewModelLocator();
        IViewsContainer InstantiateViewContainer();
        IViewLoader InstantiateViewLoader();
        void OnCreateViewsMap(IViewsContainer viewsContainer);
        void Initialize();
    }
}