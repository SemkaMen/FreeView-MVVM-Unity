using FreeView.Sample.Scripts.Controllers;
using FreeView.Scripts;
using FreeView.Scripts.ViewModels;

namespace FreeView.Sample.Scripts.ViewModels
{
    public class PlaygroundViewModel : BaseViewModel<PlaygroundNavigationArgs>
    {
        private readonly FreeViewProvider _freeViewProvider;

        private DoorController _doorController;
        private bool _isDoorOpened;
        private int _targetDoorOpens;
        private int _doorOpenCounter;

        public bool IsDoorOpened
        {
            get => _isDoorOpened;
            set => SetProperty(ref _isDoorOpened, value);
        }
        
        public int TargetDoorOpens
        {
            get => _targetDoorOpens;
            set => SetProperty(ref _targetDoorOpens, value);
        }
        
        public int DoorOpenCounter
        {
            get => _doorOpenCounter;
            set => SetProperty(ref _doorOpenCounter, value);
        }

        public PlaygroundViewModel()
        {
            _freeViewProvider = SceneContext.GetInstance().FreeViewProvider;
        }
        
        public override void Prepare(PlaygroundNavigationArgs args)
        {
            base.Prepare(args);
            _doorController = args.DoorController;
            TargetDoorOpens = 10;
            IsDoorOpened = _doorController.IsOpened;
        }
        
        public override void OnViewEnable()
        {
            base.OnViewEnable();
            _doorController.DoorStateChanged += OnDoorControllerStateChanged;
        }
        
        public override void OnViewDisable()
        {
            base.OnViewDisable();
            _doorController.DoorStateChanged -= OnDoorControllerStateChanged;
        }
        
        public void ToggleDoor()
        {
            _doorController.Toggle();
        }
        
        private void OnDoorControllerStateChanged(object sender, bool isOpened)
        {
            IsDoorOpened = isOpened;

            if (IsDoorOpened)
                DoorOpenCounter++;

            if (DoorOpenCounter >= TargetDoorOpens)
                ShowWinScreen();
        }

        private void ShowWinScreen()
        {
            _freeViewProvider.Hide<PlaygroundViewModel>();
            _freeViewProvider.Show<WinScreenViewModel>();
        }
    }
}