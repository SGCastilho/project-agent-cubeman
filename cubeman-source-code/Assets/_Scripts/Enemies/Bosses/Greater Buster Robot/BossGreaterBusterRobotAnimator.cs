using UnityEngine;

namespace Cubeman.Enemies
{
    public sealed class BossGreaterBusterRobotAnimator : BossAnimator
    {
        #region Encapsulation
        internal float ShootingPoseStartDuration { get => _shootingPoseStartDuration; }
        internal float ShockWaveDuration { get => _shockWaveDuration; }
        #endregion

        internal bool ShootingPoseAnimation
        {
            set => animator.SetBool("shootingPose", value);
        }

        [Header("Exclusive Settings")]
        [SerializeField] private AnimationClip shootingPoseStartAnim;
        [SerializeField] private AnimationClip shockWaveAnim;

        private float _shootingPoseStartDuration;
        private float _shockWaveDuration;

        private void Awake() => GetAnimationClipLength();

        private void GetAnimationClipLength()
        {
            _shootingPoseStartDuration = shootingPoseStartAnim.length;
            _shockWaveDuration = shockWaveAnim.length;
        }
    }
}
