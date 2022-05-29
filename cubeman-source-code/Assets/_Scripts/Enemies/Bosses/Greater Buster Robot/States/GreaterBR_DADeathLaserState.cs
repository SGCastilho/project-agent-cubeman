using UnityEngine;

namespace Cubeman.Enemies
{
    public sealed class GreaterBR_DADeathLaserState : State
    {
        [Header("Classes")]
        [SerializeField] private BossGreaterBusterRobotBehaviour behaviour;

        [Header("Settings")]
        [SerializeField] private State nextState;

        private bool _deathLaserStarted;
        private float _currentDeathLaserDuration;

        public override State RunCurrentState()
        {
            StartDeathLaser();

            if(_deathLaserStarted)
            {
                _currentDeathLaserDuration += Time.deltaTime;
                if(_currentDeathLaserDuration >= behaviour.ExclusiveAnimator.DeathLaserDuration)
                {
                    EndState();

                    return nextState;
                }
            }

            return this;
        }

        private void EndState()
        {
            _deathLaserStarted = false;
            _currentDeathLaserDuration = 0;

            behaviour.Sequencer.NextSequence();
        }

        private void StartDeathLaser()
        {
            if (!_deathLaserStarted)
            {
                behaviour.Movement.MoveRight = behaviour.CheckPlayerSide.IsInRightSide();
                behaviour.ExclusiveAnimator.CallAnimationTrigger("deathLaser");
                _deathLaserStarted = true;
            }
        }
    }
}
