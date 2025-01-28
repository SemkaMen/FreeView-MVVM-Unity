using FreeView.ViewModels;

namespace Sample.Scripts.ViewModels
{
    public class WinScreenViewModel : BaseViewModel
    {
        private readonly FreeView.Scripts.FreeView _freeView;

        public WinScreenViewModel()
        {
            _freeView = SceneContext.GetInstance().FreeView;
        }

        public void Reset()
        {
            _freeView.Hide<WinScreenViewModel>();
            _freeView.Show<PlaygroundViewModel>();
        }
    }
}