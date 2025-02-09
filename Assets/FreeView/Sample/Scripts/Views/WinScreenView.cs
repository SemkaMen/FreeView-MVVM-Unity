using FreeView.Sample.Scripts.ViewModels;
using FreeView.Scripts.Views;
using FreeView.Scripts.Views.Attributes;
using UnityEngine.UI;

namespace FreeView.Sample.Scripts.Views
{
    [ViewPresentation(CanvasContainerName = "MainCanvas")]
    public class WinScreenView : BaseView<WinScreenViewModel>
    {
        private Button _resetButton;
        
        protected override void OnViewAwake()
        {
            base.OnViewAwake();

            _resetButton = GetElementComponent<Button>("_resetButton");
        }

        protected override void OnViewEnable()
        {
            base.OnViewEnable();
            _resetButton?.onClick.AddListener(OnResetClicked);
        }
        
        protected override void OnViewDisable()
        {
            _resetButton?.onClick.RemoveListener(OnResetClicked);
            base.OnViewDisable();
        }
        
        private void OnResetClicked()
        {
            ViewModel.Reset();
        }
    }
}