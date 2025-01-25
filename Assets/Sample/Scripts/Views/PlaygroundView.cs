using FreeView.Bindings;
using FreeView.Views;
using FreeView.Views.Attributes;
using Sample.Scripts.ViewModels;
using TMPro;
using UnityEngine.UI;

namespace Sample.Scripts.Views
{
    [ViewPresentation(CanvasContainerName = "MainCanvas")]
    public class PlaygroundView : BaseView<PlaygroundBaseViewModel>
    {
        private Button _toggleDoorButton;
        private TextMeshProUGUI _counterText;
        
        private int _counterValue;

        public int CounterValue
        {
            get => _counterValue;
            set
            {
                _counterValue = value;
                _counterText.text = $"Counter: {value}";
            }
        }

        protected override void OnViewAwake()
        {
            base.OnViewAwake();

            _toggleDoorButton = GetElementComponent<Button>("_toggleDoorButton");
            _counterText = GetElementComponent<TextMeshProUGUI>("_counterText");
        }

        protected override void OnViewStart()
        {
            base.OnViewStart();
        
            var set = this.CreateBindingSet<PlaygroundView, PlaygroundBaseViewModel>();
            set.Bind(this).For(v => v.CounterValue).To(vm => vm.CounterValue);
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
    }
}