using FreeView.ViewModels;
using Sample.Scripts.Controllers;

namespace Sample.Scripts.ViewModels
{
    public class PlaygroundViewModel : BaseViewModel
    {
        private DoorController _doorController;
        private int _counterValue;

        public int CounterValue
        {
            get => _counterValue;
            set => SetProperty(ref _counterValue, value);
        }

        public PlaygroundViewModel()
        {
        }

        public void ToggleDoor()
        {
            _doorController.Toggle();
        }
    }
}