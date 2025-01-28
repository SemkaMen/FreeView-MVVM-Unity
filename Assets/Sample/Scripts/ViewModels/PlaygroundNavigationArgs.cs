using Sample.Scripts.Controllers;

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