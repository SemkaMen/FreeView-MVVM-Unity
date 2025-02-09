using FreeView.Sample.Scripts.Components;
using FreeView.Sample.Scripts.ViewModels;
using FreeView.Scripts.Bindings;
using FreeView.Scripts.Views;
using FreeView.Scripts.Views.Attributes;
using UnityEngine.UI;

namespace FreeView.Sample.Scripts.Views
{
    [ViewPresentation(CanvasContainerName = "MainCanvas")]
    public class PlaygroundView : BaseView<PlaygroundViewModel>
    {
        private Button _toggleDoorButton;
        private Image _doorStateImage;
        private Text _doorStateText;
        private ProgressBarComponent _doorCounterProgressBar;
        
        private bool _isDoorOpened;

        public bool IsDoorOpened
        {
            get => _isDoorOpened;
            set
            {
                _isDoorOpened = value;
                UpdateDoorState();
            }
        }

        protected override void OnViewAwake()
        {
            base.OnViewAwake();

            _toggleDoorButton = GetElementComponent<Button>("_toggleDoorButton");
            _doorStateImage = GetElementComponent<Image>("_doorStateImage");
            _doorStateText = GetElementComponent<Text>("_doorStateText");
            _doorCounterProgressBar = GetElementComponent<ProgressBarComponent>("_doorCounterProgressBar");
        }

        protected override void OnViewStart()
        {
            base.OnViewStart();
        
            var set = this.CreateBindingSet<PlaygroundView, PlaygroundViewModel>();
            set.Bind(this).For(v => v.IsDoorOpened).To(vm => vm.IsDoorOpened);
            set.Bind(_doorCounterProgressBar).For(v => v.CurrentValue).To(vm => vm.DoorOpenCounter);
            set.Bind(_doorCounterProgressBar).For(v => v.MaxValue).To(vm => vm.TargetDoorOpens);
            set.Apply();
        }

        protected override void OnViewEnable()
        {
            base.OnViewEnable();
            _toggleDoorButton?.onClick.AddListener(OnToggleDoorClicked);
        }

        protected override void OnViewDisable()
        {
            _toggleDoorButton?.onClick.RemoveListener(OnToggleDoorClicked);
            base.OnViewDisable();
        }

        private void OnToggleDoorClicked()
        {
            ViewModel.ToggleDoor();
        }

        private void UpdateDoorState()
        {
            _doorStateText.text = "Door is " + (IsDoorOpened ? "opened" : "closed");
            _doorStateImage.color = IsDoorOpened ? CustomColors.Green : CustomColors.Red;
        }
    }
}