using System;
using System.Collections;
using UnityEngine;

namespace FreeView.Sample.Scripts.Controllers
{
    public class DoorController : MonoBehaviour
    {
        [SerializeField]
        private float duration;
        
        [SerializeField]
        private Vector3 openPosition;

        private Vector3 _initialPosition;
        private bool _isOpened;
        private bool _isProcessing;

        public event Action<object, bool> DoorStateChanged;

        public bool IsOpened => _isOpened;

        private void Start()
        {
            _initialPosition = transform.position;
        }

        public void Toggle()
        {
            if (_isProcessing) return;
            StartCoroutine(ToggleDoorCoroutine());
        }

        private IEnumerator ToggleDoorCoroutine()
        {
            var elapsedTime = 0f;
            var startPosition = transform.position;
            var targetPosition = IsOpened ? _initialPosition : openPosition;

            _isProcessing = true;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                transform.position = Vector3.Lerp(startPosition, targetPosition, EaseInQuart(elapsedTime / duration));
                yield return null;
            }
            _isOpened = !IsOpened;
            DoorStateChanged?.Invoke(this, IsOpened);
            _isProcessing = false;
        }
        
        private static float EaseInQuart(float x)
        {
            return Mathf.Pow(x, 4);
        }
    }
}