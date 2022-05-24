using DG.Tweening;
using UnityEngine;

namespace Cubeman.GameCamera
{
    public sealed class CameraShake : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] [Range(0.1f, 1f)] private float lightShakeDuration = 0.2f;
        [SerializeField] [Range(0.1f, 1f)] private float lightShakeStregth = 0.2f;
        [SerializeField] [Range(1f, 10f)] private int lightShakeVibration = 2;
        [SerializeField] [Range(0f, 90f)] private float lightShakeRandomness = 20f;

        [Space(12)]

        [SerializeField] [Range(0.1f, 1f)] private float defaultShakeDuration = 0.6f;
        [SerializeField] [Range(0.1f, 1f)] private float defaultShakeStregth = 0.6f;
        [SerializeField] [Range(1f, 10f)] private int defaultShakeVibration = 6;
        [SerializeField] [Range(0f, 90f)] private float defaultShakeRandomness = 60f;

        [Space(12)]

        [SerializeField] [Range(0.1f, 1f)] private float heavyShakeDuration = 1f;
        [SerializeField] [Range(0.1f, 1f)] private float heavyShakeStregth = 1f;
        [SerializeField] [Range(1f, 10f)] private int heavyShakeVibration = 10;
        [SerializeField] [Range(0f, 90f)] private float heavyShakeRandomness = 90f;

        private Transform _transform;

        private void Awake() => SetupObject();

        private void SetupObject()
        {
            _transform = GetComponent<Transform>();
        }

        //DEBUG
        private void Update() 
        {
            if(Input.GetKeyDown(KeyCode.F1))
            {
                LightShakeCamera();
            }

            if(Input.GetKeyDown(KeyCode.F2))
            {
                ShakeCamera();
            }

            if(Input.GetKeyDown(KeyCode.F3))
            {
                HeavyShakeCamera();
            }      
        }
        //DEBUG

        public void LightShakeCamera()
            => Shake(ref lightShakeDuration, ref lightShakeStregth, ref lightShakeVibration, 
                ref lightShakeRandomness);

        public void ShakeCamera()
            => Shake(ref defaultShakeDuration, ref defaultShakeStregth, ref defaultShakeVibration, 
                ref defaultShakeRandomness);

        public void HeavyShakeCamera()
            => Shake(ref heavyShakeDuration, ref heavyShakeStregth, ref heavyShakeVibration, 
                ref heavyShakeDuration);

        private void Shake(ref float duration, ref float stregth, ref int vibration, ref float randomness)
        {
            _transform.DOKill();
            _transform.DOShakePosition(duration, stregth, vibration, randomness);
        }

        public void CustomShake(float duration, float stregth, int vibration, float randomness)
        {
            _transform.DOKill();
            _transform.DOShakePosition(duration, stregth, vibration, randomness);
        }
    }
}
