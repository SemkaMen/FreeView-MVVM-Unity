namespace FreeView.ViewModels.Interfaces
{
    public interface IBaseViewModel
    {
        void OnViewStart();
        void OnViewEnable();
        void OnViewDisable();
        void OnViewDestroy();
        void OnViewAwake();
        void Prepare();
        void Initialize();
        void Start();
    }

    public interface IBaseViewModel<in TNavigationArgs> : IBaseViewModel
    {
        void Prepare(TNavigationArgs navigationArgs);
    }
}