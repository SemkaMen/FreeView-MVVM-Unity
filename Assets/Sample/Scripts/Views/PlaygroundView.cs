using FreeView.Bindings;
using FreeView.Views;
using FreeView.Views.Attributes;
using Sample.Scripts.ViewModels;

namespace Sample.Scripts.Views
{
    [ViewPresentation(CanvasContainerName = "MainCanvas")]
    public class PlaygroundView : BaseView<PlaygroundBaseViewModel>
    {
        protected override void OnViewAwake()
        {
            base.OnViewAwake();
        }

        protected override void OnViewStart()
        {
            base.OnViewStart();
        
            var set = this.CreateBindingSet<PlaygroundView, PlaygroundBaseViewModel>();
            set.Apply();
        }
    }
}