using FreeView.Scripts.Views;
using UnityEngine;
using UnityEngine.UI;

namespace FreeView.Sample.Scripts.Components
{
    public class ProgressBarComponent : BaseViewComponent
    {
        private int _currentValue;

        [SerializeField] private Text currentValueText;
        [SerializeField] private Slider slider;

        public int CurrentValue
        {
            get => _currentValue;
            set
            {
                _currentValue = value;
                slider.value = value;
                UpdateView();
            }
        }

        public float MaxValue
        {
            get => slider.maxValue;
            set
            {
                slider.maxValue = value;
                UpdateView();
            }
        }
        
        public float MinValue
        {
            get => slider.minValue;
            set => slider.minValue = value;
        }
        
        private void UpdateView()
        {
            currentValueText.text = $"{slider.value}/{MaxValue}";
        }
    }
}