using FreeView.Sample.Scripts.Controllers;

namespace FreeView.Sample.Scripts.ViewModels
{
    public class PlaygroundNavigationArgs
    {
        public PlaygroundNavigationArgs()
        {
        }

        public PlaygroundNavigationArgs(DoorController doorController)
        {
            DoorController = doorController;
        }

        public DoorController DoorController { get; set; }
    }
}