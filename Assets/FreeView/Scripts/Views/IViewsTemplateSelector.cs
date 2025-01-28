using System;
using System.Collections.Generic;

namespace FreeView.Views
{
    public interface IViewsTemplateSelector
    {
        Dictionary<Type, Type> ViewMapping { get; }
    }
}