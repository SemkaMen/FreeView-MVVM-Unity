using Core.MVVM.Services;
using Core.MVVM.Services.Interfaces;
using FreeView.Scripts.ViewModels.Interfaces;
using FreeView.Scripts.ViewPresenter.Interfaces;

namespace FreeView.Scripts.Services
{
    //TODO combine with canvas service
    public class SceneViewService : ISceneViewService
    {
        private readonly IViewPresenter _viewPresenter;
        private readonly ICanvasViewService _canvasViewService;

        public SceneViewService(IViewPresenter viewPresenter, ICanvasViewService canvasViewService)
        {
            _viewPresenter = viewPresenter;
            _canvasViewService = canvasViewService;
        }

        public void InitView<TViewModel>(bool isVisible = true) where TViewModel : IBaseViewModel
        {
            _canvasViewService.AddView(_viewPresenter.PresentView<TViewModel>(), isVisible);
        }

        public void InitView<TViewModel, TNavigationArgs>(TNavigationArgs navigationArgs, bool isVisible = true) where TViewModel : IBaseViewModel<TNavigationArgs>  
        {
            _canvasViewService.AddView(_viewPresenter.PresentView<TViewModel, TNavigationArgs>(navigationArgs), isVisible);
        }
    }
}