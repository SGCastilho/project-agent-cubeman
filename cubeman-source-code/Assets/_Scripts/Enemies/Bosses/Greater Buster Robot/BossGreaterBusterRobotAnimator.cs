using UnityEngine;

namespace Cubeman.Enemies
{
    public sealed class BossGreaterBusterRobotAnimator : BossAnimator
    {
        #region Encapsulation
        internal float ShootingPoseStartDuration { get => _shootingPoseStartDuration; }
        internal float ShockWaveDuration { get => _shockWaveDuration; }
        internal float OffensiveRunStartDuration { get => _offensiveRunStartDuration; }

        internal float DeathLaserDuration { get => _deathLaserDuration; }
        #endregion

        internal bool ShootingPoseAnimation
        {
            set => animator.SetBool("shootingPose", value);
        }

        internal bool RunningAnimation
        {
            set => animator.SetBool("running", value);
        }

        [Header("Exclusive Classes")]
        [SerializeField] private BossGreaterBusterRobotBehaviour behaviour;

        [Header("Exclusive Settings")]
        [SerializeField] private AnimationClip shootingPoseStartAnim;
        [SerializeField] private AnimationClip shockWaveAnim;
        [SerializeField] private AnimationClip offensiveRunStartAnim;

        [Space(12)]

        [SerializeField] private AnimationClip deathLaserInAnim;
        [SerializeField] private AnimationClip deathLaserOutAnim;

        private float _shootingPoseStartDuration;
        private float _shockWaveDuration;
        private float _offensiveRunStartDuration;

        private float _deathLaserDuration;

        private void Awake() => GetAnimationClipLength();

        private void GetAnimationClipLength()
        {
            _shootingPoseStartDuration = shootingPoseStartAnim.length;
            _shockWaveDuration = shockWaveAnim.length;
            _offensiveRunStartDuration = offensiveRunStartAnim.length;

            _deathLaserDuration = deathLaserInAnim.length + deathLaserOutAnim.length;
        }

        private void Update() => ConstantAnimation();

        private void ConstantAnimation()
        {
            animator.SetBool("isGrounded", behaviour.Moviment.Gravity.IsGrounded);
        }
    }
}
