using UnityEngine;

namespace Cubeman.Enemies 
{
    public class BusterRobotShootingState : State
    {
        [Header("Classes")]
        [SerializeField] private EnemyBusterRobotBehaviour behaviour;

        [Header("Settings")]
        [SerializeField] private ChasingState chasingState;

        [Space(12)]

        [SerializeField] [Range(0.1f, 1f)] private float shootingPoseDelay = 1f;
        [SerializeField] [Range(0.1f, 2f)] private float shootingFireRate = 1.2f;

        [Space(12)]

        [SerializeField] [Range(0.1f, 1f)] private float backChasingDelay = 1f;

        private bool _canShoot = true;
        private bool _shootingPose;

        private bool _inBackChasingDelay;

        private float _currentShootingFireRate;
        private float _currentShootingPoseDelay;
        private float _currentBackChasingDelay;

        private float _distanceFromPlayer;

        public override State RunCurrentState()
        {
            if (_inBackChasingDelay)
            {
                _currentBackChasingDelay += Time.deltaTime;
                if (_currentBackChasingDelay >= backChasingDelay)
                {
                    behaviour.Animator.RunningAnimation = true;
                    behaviour.Animator.ShootingPoseAnimation = false;

                    ResetTimers();
                    ResetBooleans();

                    return chasingState;
                }
            }
            else { _currentBackChasingDelay = 0; }

            Shooting();

            BackChasingCheck();

            return this;
        }

        private void Shooting()
        {
            if (!_shootingPose)
            {
                behaviour.Animator.ShootingPoseAnimation = true;

                _currentShootingPoseDelay += Time.deltaTime;
                if (_currentShootingPoseDelay >= shootingPoseDelay)
                {
                    _shootingPose = true;
                    _currentShootingPoseDelay = 0;
                }
            }
            else
            {
                ShootingFireRate();
            }
        }

        private void ShootingFireRate()
        {
            if (!_shootingPose) return;

            if (!_canShoot)
            {
                _currentShootingFireRate += Time.deltaTime;
                if (_currentShootingFireRate >= shootingFireRate)
                {
                    _canShoot = true;
                }
            }
            else
            {
                behaviour.Moviment.MoveRight = behaviour.PlayerSide.IsInRightSide();
                behaviour.Animator.CallAnimationTrigger("shoot");

                _canShoot = false;
                _currentShootingFireRate = 0;
            }
        }

        private void BackChasingCheck()
        {
            if (_inBackChasingDelay)
            {
                CheckIfPlayerEnterShootingRange();
            }
            else
            {
                CheckIfPlayerExitShootingRange();
            }
        }

        private void CheckIfPlayerEnterShootingRange()
        {
            _distanceFromPlayer = behaviour.CheckPlayerDistance.PlayerDistance();

            if (_distanceFromPlayer < chasingState.AttackingRange)
            {
                _inBackChasingDelay = false;
            }
        }

        private void CheckIfPlayerExitShootingRange()
        {
            _distanceFromPlayer = behaviour.CheckPlayerDistance.PlayerDistance();

            if (_distanceFromPlayer > chasingState.AttackingRange)
            {
                _inBackChasingDelay = true;
            }
        }

        private void OnDisable() => ResetState();

        private void ResetState()
        {
            ResetTimers();
            ResetBooleans();
            ResetDistanceCounter();
        }

        private void ResetBooleans()
        {
            _canShoot = true;
            _shootingPose = false;
        }

        private void ResetTimers()
        {
            _currentShootingFireRate = 0;
            _currentShootingPoseDelay = 0;
            _currentBackChasingDelay = 0;
        }

        private void ResetDistanceCounter()
        {
            _distanceFromPlayer = 0;
        }
    }
}