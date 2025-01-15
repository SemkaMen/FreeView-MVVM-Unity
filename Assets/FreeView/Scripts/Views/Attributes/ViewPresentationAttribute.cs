using System;

namespace FreeView.Views.Attributes
{
    public class ViewPresentationAttribute : Attribute
    {
        public string CanvasContainerName { get; set; }
        public string PrefabPath { get; set; }
    }
}