using System.Collections.Generic;
using FreeView.ViewModels.Interfaces;
using FreeView.Views;
using FreeView.Views.Interfaces;
using Sample.Scripts.Controllers;
using Sample.Scripts.ViewModels;
using Sample.Scripts.Views;
using UnityEngine;


public class Template : BaseTemplateSelector
{
    protected override Dictionary<IBaseViewModel, IBaseView> ViewMapping => new()
    {
        { typeof(PlaygroundViewModel), typeof(PlaygroundView) },
    };
}

public class SceneContext : MonoBehaviour
{
    [SerializeField] private DoorController doorController;

    private static SceneContext _instance;
    private static bool _applicationIsQuitting;
    
    public FreeView.Scripts.FreeView FreeView { get; private set; }

    public static SceneContext GetInstance()
    {
        if (_applicationIsQuitting)
            return null;

        if (_instance == null)
        {
            _instance = FindObjectOfType<SceneContext>();
            if (_instance == null)
            {
                var obj = new GameObject();
                obj.name = nameof(SceneContext);
                _instance = obj.AddComponent<SceneContext>();
            }
        }
        return _instance;
    }
    
    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else if (_instance != this)
            Destroy(gameObject);
        
        FreeView = new FreeView.Scripts.FreeView();
    }
    
    private void OnApplicationQuit()
    {
        _applicationIsQuitting = true;
    }
}