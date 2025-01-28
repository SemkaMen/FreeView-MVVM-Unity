using FreeView.Services;
using FreeView.Services.Interfaces;
using FreeView.ViewModelLocator;
using FreeView.ViewModelLocator.Interfaces;
using FreeView.ViewModels.Interfaces;
using FreeView.ViewPresenter.Interfaces;
using FreeView.Views;
using FreeView.Views.Interfaces;

namespace FreeView.Scripts
{
    public class FreeView
    {
        private readonly ICanvasService _canvasService;

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

        public FreeView()
        {
            _canvasService = CreateCanvasService();
        }
        
        public FreeView(IViewsTemplateSelector viewsTemplateSelector) : this()
        {
            CreateViewMappingFromSelector(viewsTemplateSelector);
        }
        
        public void Hide<TViewModel>() where TViewModel : IBaseViewModel
        {
            _canvasService.Hide<TViewModel>();
        }
        
        public void Show<TViewModel>() where TViewModel : IBaseViewModel
        {
            _canvasService.Show<TViewModel>();
        }

        public void Show<TViewModel, TNavigationArgs>(TNavigationArgs navigationArgs)
            where TViewModel : IBaseViewModel<TNavigationArgs>
        {
            _canvasService.Show<TViewModel, TNavigationArgs>(navigationArgs);
        }

        public virtual void RegisterViewsSelector(IViewsTemplateSelector templateSelector)
        {
            
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
        
        protected virtual ICanvasService InstantiateCanvasService()
        {
            return new CanvasService(ViewPresenter, ViewsContainer);
        }
        
        private ICanvasService CreateCanvasService()
        {
            return InstantiateCanvasService();
        }

        private void CreateViewMappingFromSelector(IViewsTemplateSelector viewsTemplateSelector)
        {
            foreach (var map in viewsTemplateSelector.ViewMapping) 
                ViewsContainer.Add(map.Key, map.Value);
        }
    }
}