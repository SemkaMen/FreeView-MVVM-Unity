using System;
using System.Collections;
using System.Collections.Generic;
using FreeView.Scripts;
using FreeView.Views.Interfaces;
using Sample.Scripts.Controllers;
using Sample.Scripts.ViewModels;
using Sample.Scripts.Views;
using UnityEngine;

public class SceneContext : MonoBehaviour
{
    [SerializeField] private DoorController _doorController;
    
    private Setup _setup;

    private void Awake()
    {
        _setup = new Setup();
    }

    // Start is called before the first frame update
    void Start()
    {
        _setup.Service.Show<PlaygroundViewModel>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class Setup : FreeViewSetup
{
    public override void OnCreateViewsMap(IViewsContainer viewsContainer)
    {
        viewsContainer.Add<PlaygroundViewModel, PlaygroundView>();
    }

    public Setup() : base()
    {
        
    }
    public Setup(Configuration configuration) : base(configuration)
    {
    }
}
