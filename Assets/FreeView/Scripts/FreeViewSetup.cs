using FreeView.Services;
using FreeView.Services.Interfaces;
using FreeView.ViewModelLocator;
using FreeView.ViewModelLocator.Interfaces;
using FreeView.ViewPresenter.Interfaces;
using FreeView.Views;
using FreeView.Views.Interfaces;

namespace FreeView.Scripts
{
    /// <summary>
    /// Entry point class for initialization
    /// </summary>
    public abstract class FreeViewSetup
    {
        private readonly CanvasService _canvasService;

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

        public CanvasService Service => _canvasService;

        public FreeViewSetup()
        {
            Initialize();
            _canvasService = new CanvasService(ViewPresenter);
        }
        
        public abstract void OnCreateViewsMap(IViewsContainer viewsContainer);

        public void Initialize()
        {
            OnCreateViewsMap(ViewsContainer);
        }

        protected virtual IViewPresenter InstantiateViewPresenter()
        {
            return new ViewPresenter.ViewPresenter(ViewLoader, ViewModelLocator, ViewsContainer);
        }

        protected virtual IViewModelProvider InstantiateViewModelProvider()
        {
            return new ViewModelProvider();
        }

        protected virtual IViewModelLocator InstantiateViewModelLocator()
        {
            return new ViewModelLocator.ViewModelLocator(ViewModelProvider);
        }

        protected virtual IViewsContainer InstantiateViewContainer()
        {
            return new ViewsContainer();
        }

        protected virtual IViewLoader InstantiateViewLoader()
        {
            return new ViewLoader(ViewsContainer);
        }
        
        protected virtual ICanvasService CreateCanvasService()
        {
            return new CanvasService(ViewPresenter);
        }
    }
}