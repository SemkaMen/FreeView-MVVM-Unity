using FreeView.ViewModels;
using UnityEngine.SceneManagement;

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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}