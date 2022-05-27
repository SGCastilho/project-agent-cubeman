using UnityEngine;

namespace Cubeman.Enemies
{
    public sealed class GreaterBR_ShootingPostionState : State
    {
        [Header("Classes")]
        [SerializeField] private BossGreaterBusterRobotBehaviour behaviour;

        [Header("Settings")]
        [SerializeField] private State nextState;

        [Space(12)]

        [SerializeField] [Range(4, 12)] private int maxShooting = 4;
        [SerializeField] [Range(0.1f, 1f)] private float fireRate = 1f;

        private bool _inShootingPose;
        private float _currentShootingPoseStartDuration;

        private int _currentShoot;
        private float _currentFireRate;

        public override State RunCurrentState()
        {
            ShootingPoseTimer();

            if(_inShootingPose)
            {
                if(_currentShoot < maxShooting)
                {
                    FireRateTimer();
                }
                else if(_currentShoot >= maxShooting)
                {
                    EndState();

                    return nextState;
                }
            }

            return this;
        }

        private void EndState()
        {
            _inShootingPose = false;

            behaviour.ExclusiveAnimator.ShootingPoseAnimation = false;

            _currentShoot = 0;
            _currentFireRate = 0;

            behaviour.Sequencer.NextSequence();
        }

        private void FireRateTimer()
        {
            _currentFireRate += Time.deltaTime;
            if (_currentFireRate >= fireRate)
            {
                Shoot();
            }
        }

        private void Shoot()
        {
            behaviour.Movement.FlipEnemy(behaviour.CheckPlayerSide.IsInRightSide());
            behaviour.ExclusiveAnimator.CallAnimationTrigger("shoot");
            _currentFireRate = 0;
            _currentShoot++;
        }

        private void ShootingPoseTimer()
        {
            if (!_inShootingPose)
            {
                behaviour.ExclusiveAnimator.ShootingPoseAnimation = true;

                _currentShootingPoseStartDuration += Time.deltaTime;
                if (_currentShootingPoseStartDuration >= behaviour.ExclusiveAnimator.ShootingPoseStartDuration)
                {
                    behaviour.Movement.FlipEnemy(behaviour.CheckPlayerSide.IsInRightSide());
                    _currentShootingPoseStartDuration = 0;
                    _inShootingPose = true;
                }
            }
        }
    }
}
