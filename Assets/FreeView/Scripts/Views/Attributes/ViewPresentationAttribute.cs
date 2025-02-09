using System;

namespace FreeView.Scripts.Views.Attributes
{
    public class ViewPresentationAttribute : Attribute
    {
        public string CanvasContainerName { get; set; }
        public string PrefabPath { get; set; }
    }
}