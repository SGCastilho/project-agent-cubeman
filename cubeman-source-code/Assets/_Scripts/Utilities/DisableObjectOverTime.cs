using UnityEngine;

namespace Cubeman.Utilities
{
    public sealed class DisableObjectOverTime : MonoBehaviour
    {
        #region Encapsulation
        public float DisableTime { set => disableTime = value; }
        #endregion

        [Header("Settings")]
        [SerializeField] [Range(1f, 20f)] private float disableTime = 6f;
        private float _currentDisableTime;

        private void OnDisable() => ResetTimer();

        public void ResetTimer()
        {
            _currentDisableTime = 0;
        }

        private void Update() 
        {
            _currentDisableTime += Time.deltaTime;
            if(_currentDisableTime >= disableTime)
            {
                gameObject.SetActive(false);
                _currentDisableTime = 0;
            }
        }
    }
}
