using System;
using System.Collections.Generic;
using FreeView.Sample.Scripts.ViewModels;
using FreeView.Sample.Scripts.Views;
using FreeView.Scripts.Views;

namespace FreeView.Sample.Scripts
{
    public class SampleViewsTemplateSelector : BaseViewsTemplateSelector
    {
        public override Dictionary<Type, Type> ViewMapping => new()
        {
            { typeof(PlaygroundViewModel), typeof(PlaygroundView) },
            { typeof(WinScreenViewModel), typeof(WinScreenView) },
        };
    }
}