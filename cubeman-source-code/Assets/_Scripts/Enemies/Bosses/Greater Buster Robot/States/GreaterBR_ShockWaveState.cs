using UnityEngine;

namespace Cubeman.Enemies
{
    public sealed class GreaterBR_ShockWaveState : State
    {
        [Header("Classes")]
        [SerializeField] private BossGreaterBusterRobotBehaviour behaviour;

        [Header("Settings")]
        [SerializeField] private State nextState;

        private bool _shockWaveStarted;
        private float _currentShockWaveDuration;

        public override State RunCurrentState()
        {
            StartShockWave();

            _currentShockWaveDuration += Time.deltaTime;
            if (_currentShockWaveDuration >= behaviour.ExclusiveAnimator.ShockWaveDuration)
            {
                EndState();

                return nextState;
            }

            return this;
        }

        private void EndState()
        {
            _currentShockWaveDuration = 0;
            _shockWaveStarted = false;

            behaviour.Sequencer.NextSequence();
        }

        private void StartShockWave()
        {
            if (!_shockWaveStarted)
            {
                behaviour.Movement.FlipEnemy(behaviour.CheckPlayerSide.IsInRightSide());
                behaviour.Animator.CallAnimationTrigger("shockWave");
                _shockWaveStarted = true;
            }
        }
    }
}
