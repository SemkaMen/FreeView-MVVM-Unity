using FreeView.Scripts.ViewModels.Interfaces;

namespace Core.MVVM.Services
{
    public interface ISceneViewService
    {
        void InitView<TViewModel>(bool isVisible = true) where TViewModel : IBaseViewModel;
        void InitView<TViewModel, TNavigationArgs>(TNavigationArgs navigationArgs, bool isVisible = true) 
            where TViewModel : IBaseViewModel<TNavigationArgs>;
    }
}