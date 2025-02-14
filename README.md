# FreeView-MVVM-Unity

FreeView is a lightweight framework for Unity that enables MVVM-based UI construction without using the Unity Inspector.

## Installation

You can install FreeView in two ways:

Download the package via GitHub.

Install the unitypackage file manually.

## Usage

1. **Initialize FreeView**

Create an instance of the FreeViewProvider class in your game's entry point. Add dictionary that describes mapping for view and viewmodels.

    ```csharp
    public class SceneContext : MonoBehaviour
    {
        private void Awake()
        {
            FreeViewProvider = new FreeViewProvider(new()
            {
                { typeof(PlaygroundViewModel), typeof(PlaygroundView) },
            });
        }
    }
    ```
You can alse create mapping for views and viewmodels inheriting BaseViewsTemplateSelector class:

    ```csharp
    public class SampleViewsTemplateSelector : BaseViewsTemplateSelector
    {
        public override Dictionary<Type, Type> ViewMapping => new()
        {
            { typeof(PlaygroundViewModel), typeof(PlaygroundView) }
        };
    }
    ```

2. **Show and Hide Views**

Call the Show and Hide methods of the FreeView instance to display or remove views.

    ```csharp
    private void Start()
    {
        FreeViewProvider.Show<PlaygroundViewModel>();
    }
    ```

3. **Create a View Prefab**

Create a prefab for your view.

The prefab must contain a Canvas and RectTransform component.

Place the prefab in the following directory: <root>/Resources/Prefabs/Views.

Name view elements with a prefix _ (e.g., _buttonPlay, _labelScore).

4. **Create a View and ViewModel**

Create classes that inherits from BaseView for your view and BaseViewModel to create data transfer object.

5. **Working With Views**

Override the OnViewAwake method to locate UI components using the GetElementComponent<T> method:

    ```csharp
    private Button _toggleDoorButton;
    private Image _doorStateImage;
    private Text _doorStateText;
    private ProgressBarComponent _doorCounterProgressBar;

    //...
    //...
    //...

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

    private void UpdateDoorState()
    {
        _doorStateText.text = "Door is " + (IsDoorOpened ? "opened" : "closed");
        _doorStateImage.color = IsDoorOpened ? CustomColors.Green : CustomColors.Red;
    }
    ```

6. **Data Binding**

Use the OnViewStart method to bind data between the view and view model:

    ```csharp
    protected override void OnViewStart()
    {
        base.OnViewStart();

        var set = this.CreateBindingSet<PlaygroundView, PlaygroundViewModel>();
        set.Bind(this).For(v => v.IsDoorOpened).To(vm => vm.IsDoorOpened);
        set.Bind(_doorCounterProgressBar).For(v => v.CurrentValue).To(vm => vm.DoorOpenCounter);
        set.Bind(_doorCounterProgressBar).For(v => v.MaxValue).To(vm => vm.TargetDoorOpens);
        set.Apply();
    }
    ```
7. **Custom components**

Use BaseViewComponent class to create custom controls

    ```csharp
    public class ProgressBarComponent : BaseViewComponent
    {
        private int _currentValue;

        [SerializeField] private Text currentValueText;
        [SerializeField] private Slider slider;
        //...
    }
    ```

## Contributing

Feel free to contribute by submitting issues and pull requests.

## License

This project is licensed under the MIT License.