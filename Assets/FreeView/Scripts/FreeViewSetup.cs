using FreeView.Services;
using FreeView.Services.Interfaces;
using FreeView.ViewModelLocator;
using FreeView.ViewModelLocator.Interfaces;
using FreeView.ViewPresenter.Interfaces;
using FreeView.Views;
using FreeView.Views.Interfaces;

namespace FreeView.Scripts
{
    public class Configuration
    {
        public string ViewPrefabsPath { get; set; }
    }

    /// <summary>
    /// Entry point class for initialization
    /// </summary>
    public abstract class FreeViewSetup : IFreeViewSetup
    {
        private readonly Configuration _configuration;
        public CanvasService CanvasService;
        
        private IViewsContainer _viewsContainer;
        private IViewModelProvider _viewModelProvider;
        private IViewModelLocator _viewModelLocator;
        private IViewLoader _viewLoader;
        private IViewPresenter _viewPresenter;

        protected IViewsContainer ViewsContainer => _viewsContainer ??= InstantiateViewContainer();
        protected IViewModelProvider ViewModelProvider => _viewModelProvider ??= InstantiateViewModelProvider();
        protected IViewModelLocator ViewModelLocator => _viewModelLocator ??= InstantiateViewModelLocator();
        protected IViewLoader ViewLoader => _viewLoader ??= InstantiateViewLoader();
        protected IViewPresenter ViewPresenter => _viewPresenter ??= InstantiateViewPresenter();

        public CanvasService Service => CanvasService;

        public FreeViewSetup()
        {
            Initialize();
            CanvasService = new CanvasService(ViewPresenter);
        }

        public FreeViewSetup(Configuration configuration) : base()
        {
            _configuration = configuration;
        }

        public void Initialize()
        {
            OnCreateViewsMap(ViewsContainer);
        }

        public virtual IViewPresenter InstantiateViewPresenter()
        {
            return new ViewPresenter.ViewPresenter(ViewLoader, ViewModelLocator, ViewsContainer);
        }

        public virtual IViewModelProvider InstantiateViewModelProvider()
        {
            return new ViewModelProvider();
        }

        public virtual IViewModelLocator InstantiateViewModelLocator()
        {
            return new ViewModelLocator.ViewModelLocator(ViewModelProvider);
        }

        public virtual IViewsContainer InstantiateViewContainer()
        {
            return new ViewsContainer();
        }

        public virtual IViewLoader InstantiateViewLoader()
        {
            return new ViewLoader(ViewsContainer);
        }

        public abstract void OnCreateViewsMap(IViewsContainer viewsContainer);

        private ICanvasService CreateCanvasService()
        {
            return new CanvasService(ViewPresenter);
        }
    }

    public interface IFreeViewSetup
    {
        IViewPresenter InstantiateViewPresenter();
        IViewModelProvider InstantiateViewModelProvider();
        IViewModelLocator InstantiateViewModelLocator();
        IViewsContainer InstantiateViewContainer();
        IViewLoader InstantiateViewLoader();
        void OnCreateViewsMap(IViewsContainer viewsContainer);
        void Initialize();
    }
}