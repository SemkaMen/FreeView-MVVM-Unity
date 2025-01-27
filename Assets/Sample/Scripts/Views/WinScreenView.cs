using FreeView.Views;
using FreeView.Views.Attributes;
using Sample.Scripts.ViewModels;

namespace Sample.Scripts.Views
{
    [ViewPresentation(CanvasContainerName = "MainCanvas")]
    public class WinScreenView : BaseView<WinScreenViewModel>
    {
    }
}