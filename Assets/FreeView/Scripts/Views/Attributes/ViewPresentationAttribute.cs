using System;

namespace Core.MVVM.Views.Attributes
{
    public class ViewPresentationAttribute : Attribute
    {
        public string ParentCanvasContainer { get; set; }
        public string PrefabPath { get; set; }
    }
}