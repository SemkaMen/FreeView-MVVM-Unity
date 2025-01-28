using System;
using System.Collections.Generic;

namespace FreeView.Views
{
    public abstract class BaseViewsTemplateSelector : IViewsTemplateSelector
    {
        public abstract Dictionary<Type, Type> ViewMapping { get; }
    }
}