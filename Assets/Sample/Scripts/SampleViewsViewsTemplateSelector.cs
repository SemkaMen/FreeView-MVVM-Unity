using System;
using System.Collections.Generic;
using FreeView.Views;
using Sample.Scripts.ViewModels;
using Sample.Scripts.Views;

public class SampleViewsViewsTemplateSelector : BaseViewsTemplateSelector
{
    public override Dictionary<Type, Type> ViewMapping => new()
    {
        { typeof(PlaygroundViewModel), typeof(PlaygroundView) },
    };
}