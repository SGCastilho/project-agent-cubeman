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
                behaviour.Moviment.IsMoving = true;

                if(behaviour.CheckWallInFront.HasWallInFront(behaviour.Moviment.MoveRight))
                {
                    EndState();

                    return nextState;
                }
            }

            return this;
        }

        private void EndState()
        {
            behaviour.Moviment.IsMoving = false;
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
                    behaviour.Moviment.MoveRight = behaviour.CheckPlayerSide.IsInRightSide();

                    _currentOffensiveRunStartDuration = 0;
                    _offensiveRunPrepared = true;
                }
            }
        }
    }
}
