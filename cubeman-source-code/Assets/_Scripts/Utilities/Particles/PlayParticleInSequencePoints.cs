using System.Collections;
using UnityEngine;

namespace Cubeman.Utilities
{
    public sealed class PlayParticleInSequencePoints : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private ParticleSystem particleToEmit;

        [Space(12)]

        [SerializeField] private Transform[] emissionPoints;

        [Space(6)]

        [SerializeField] [Range(0.1f, 6f)] private float delayBetweenEmissions = 1f;

        private bool _isPlaying;
        private bool _inverseEmissionPoints;

        private int _maxPoints;
        private int _currentPoint;
        private float _currentDelayBetweenEmissions;

        private void Awake() => CacheVariables();

        private void CacheVariables()
        {
            _maxPoints = emissionPoints.Length-1;
        }

        public void PlayParticle()
        {
            _isPlaying = true;
            _currentPoint = 0;
            _currentDelayBetweenEmissions = 0;
        }

        public void PlayParticle(float delay)
        {
            _currentPoint = 0;
            _currentDelayBetweenEmissions = 0;

            StartCoroutine(PlayParticleDelayCouroutine(delay));
        }

        IEnumerator PlayParticleDelayCouroutine(float delay)
        {
            yield return new WaitForSeconds(delay);

            _isPlaying = true;
        }

        public void StopParticle()
        {
            _isPlaying = false;
            _inverseEmissionPoints = false;

            _currentPoint = 0;
            _currentDelayBetweenEmissions = 0;
        }

        private void Update() => DelayTimer();

        private void DelayTimer()
        {
            if (!_isPlaying) return;

            _currentDelayBetweenEmissions += Time.deltaTime;
            if (_currentDelayBetweenEmissions >= delayBetweenEmissions)
            {
                EmitParticle();

                _currentDelayBetweenEmissions = 0;
            }
        }

        private void EmitParticle()
        {
            CheckCurrentPoints();
            AdjustCurrentPoints();

            particleToEmit.transform.localPosition = emissionPoints[_currentPoint].localPosition;
            particleToEmit.Play();
        }

        private void CheckCurrentPoints()
        {
            if (_currentPoint >= _maxPoints)
            {
                _inverseEmissionPoints = true;
            }

            if (_currentPoint <= 0)
            {
                _inverseEmissionPoints = false;
            }
        }

        private void AdjustCurrentPoints()
        {
            if (_inverseEmissionPoints)
            {
                _currentPoint--;
            }
            else
            {
                _currentPoint++;
            }
        }
    }
}
