using System;
using System.Collections.Generic;
using FreeView.Scripts.Views.Interfaces;

namespace FreeView.Scripts.Views
{
    public abstract class BaseViewsTemplateSelector : IViewsTemplateSelector
    {
        public abstract Dictionary<Type, Type> ViewMapping { get; }
    }
}