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
    [SerializeField] private DoorController doorController;

    private Setup _setup;

    private void Awake()
    {
        _setup = new Setup();
    }

    void Start()
    {
        _setup.Service.Show<PlaygroundViewModel, PlaygroundNavigationArgs>(new PlaygroundNavigationArgs(doorController));
    }
}

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

public class Setup : FreeViewSetup
{
    public override void OnCreateViewsMap(IViewsContainer viewsContainer)
    {
        viewsContainer.Add<PlaygroundViewModel, PlaygroundView>();
    }

    public Setup() : base()
    {
    }
}