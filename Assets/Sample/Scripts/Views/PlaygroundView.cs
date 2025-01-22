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
        private Button _rotateCardButton;
        private TextMeshProUGUI _scoreText;
        
        protected override void OnViewAwake()
        {
            base.OnViewAwake();

            _rotateCardButton = GetElementComponent<Button>("_");
            _scoreText = GetElementComponent<TextMeshProUGUI>("_");
        }

        protected override void OnViewStart()
        {
            base.OnViewStart();
        
            var set = this.CreateBindingSet<PlaygroundView, PlaygroundBaseViewModel>();
            set.Apply();
        }
    }
}