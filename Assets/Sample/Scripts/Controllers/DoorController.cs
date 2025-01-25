using System.Collections;
using UnityEngine;

namespace Sample.Scripts.Controllers
{
    public class DoorController : MonoBehaviour
    {
        [SerializeField]
        private float duration;
        
        [SerializeField]
        private Vector3 openPosition;

        private Vector3 _initialPosition;
        private bool _isOpen;
        private bool _isProcessing;

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
            var targetPosition = _isOpen ? _initialPosition : openPosition;

            _isProcessing = true;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                transform.position = Vector3.Lerp(startPosition, targetPosition, EaseInQuart(elapsedTime / duration));
                yield return null;
            }
            _isOpen = !_isOpen;
            _isProcessing = false;
        }
        
        private static float EaseInQuart(float x)
        {
            return Mathf.Pow(x, 4);
        }
    }
}