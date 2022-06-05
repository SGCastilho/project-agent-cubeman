using Cubeman.UI;
using UnityEngine;

namespace Cubeman.Manager
{
    public sealed class FPSCounterManager : MonoBehaviour
    {
        #region Encapsulation
        public int FrameRate { get => _frameRate; }
        #endregion

        public delegate void FPSChange(ref int frameRate);
        public event FPSChange OnFPSChange;

        [Header("Classes")]
        [SerializeField] private UIFPSCounter uiFPSCounter;

        private const float CALCULATE_FRAME_RATE_DURATION = 1f;
        private float _currentFrameRateDuration;

        private int _frameRate;
        private int _frameCount;

        private void OnEnable() => EnableEvents();

        private void EnableEvents()
        {
            OnFPSChange += uiFPSCounter.RefreshFPS;
        }

        private void OnDestroy() => DisableEvents();

        private void DisableEvents()
        {
            OnFPSChange -= uiFPSCounter.RefreshFPS;
        }

        private void Update() => CalculateFrameRate();

        private void CalculateFrameRate()
        {
            _currentFrameRateDuration += Time.unscaledDeltaTime;

            _frameCount++;

            if (_currentFrameRateDuration >= CALCULATE_FRAME_RATE_DURATION)
            {
                _frameRate = Mathf.RoundToInt(_frameCount / _currentFrameRateDuration);

                OnFPSChange?.Invoke(ref _frameRate);

                _frameCount = 0;
                _currentFrameRateDuration = 0;
            }
        }
    }
}