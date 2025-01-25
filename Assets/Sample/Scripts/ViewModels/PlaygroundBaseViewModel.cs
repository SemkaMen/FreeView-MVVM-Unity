using FreeView.ViewModels;
using Sample.Scripts.Controllers;

namespace Sample.Scripts.ViewModels
{
    public class PlaygroundBaseViewModel : BaseViewModel
    {
        private DoorController _doorController;
        private int _counterValue;

        public int CounterValue
        {
            get => _counterValue;
            set => SetProperty(ref _counterValue, value);
        }

        public PlaygroundBaseViewModel()
        {
        }

        public void ToggleDoor()
        {
            _doorController.Toggle();
        }
    }
}