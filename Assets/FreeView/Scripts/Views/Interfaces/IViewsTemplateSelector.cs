using System;
using System.Collections.Generic;

namespace FreeView.Scripts.Views.Interfaces
{
    public interface IViewsTemplateSelector
    {
        Dictionary<Type, Type> ViewMapping { get; }
    }
}