using UnityEngine;

namespace Cubeman.Enemies
{
    public sealed class GreaterBR_OffensiveRunState : State
    {
        [Header("Classes")]
        [SerializeField] private BossGreaterBusterRobotBehaviour behaviour;

        [Header("Settings")]
        [SerializeField] private State nextState;

        private bool _offensiveRunPrepared;
        private float _currentOffensiveRunStartDuration;

        public override State RunCurrentState()
        {
            OffensiveRunPrepareTimer();

            if(_offensiveRunPrepared)
            {
                behaviour.Movement.IsMoving = true;

                if(behaviour.CheckWallInFront.HasWallInFront(behaviour.Movement.MoveRight))
                {
                    EndState();

                    return nextState;
                }
            }

            return this;
        }

        private void EndState()
        {
            behaviour.Movement.IsMoving = false;
            behaviour.ExclusiveAnimator.RunningAnimation = false;

            _offensiveRunPrepared = false;

            behaviour.Sequencer.NextSequence();
        }

        private void OffensiveRunPrepareTimer()
        {
            if (!_offensiveRunPrepared)
            {
                behaviour.ExclusiveAnimator.RunningAnimation = true;

                _currentOffensiveRunStartDuration += Time.deltaTime;
                if (_currentOffensiveRunStartDuration >= behaviour.ExclusiveAnimator.OffensiveRunStartDuration)
                {
                    behaviour.Movement.MoveRight = behaviour.CheckPlayerSide.IsInRightSide();

                    _currentOffensiveRunStartDuration = 0;
                    _offensiveRunPrepared = true;
                }
            }
        }
    }
}
