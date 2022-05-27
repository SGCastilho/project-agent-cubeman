using UnityEngine;

namespace Cubeman.Enemies
{
    public class GreaterBR_JumpBackRepositionState : State
    {
        [Header("Classes")]
        [SerializeField] private BossGreaterBusterRobotBehaviour behaviour;

        [Header("Settings")]
        [SerializeField] private State nextState;

        [Space(12)]

        [SerializeField] [Range(10f, 40f)] private float jumpForce = 20f;
        [SerializeField] [Range(0.1f, 1f)] private float groundCheckTick = 0.4f;

        [Space(6)]

        [SerializeField] [Range(0.1f, 6f)] private float minDistanceFromPlayer = 4f;

        private bool _isJumped;
        private float _currentDistanceFromPlayer;
        private float _currentGroundCheckTick;

        #region Editor Variable
    #if UNITY_EDITOR
        [SerializeField] private bool _showMinDistanceGizmos;
    #endif
        #endregion

        public override State RunCurrentState()
        {
            if(!_isJumped)
            {
                if(behaviour.CheckWallInFront.HasWallInBackwords(behaviour.Movement.MoveRight))
                {
                    EndState();

                    return nextState;
                }

                _currentDistanceFromPlayer = behaviour.CheckPlayerDistance.PlayerDistance();
                if(_currentDistanceFromPlayer > minDistanceFromPlayer)
                {
                    EndState();

                    return nextState;
                }
            }

            Jump();

            if (_isJumped)
            {
                _currentGroundCheckTick += Time.deltaTime;
                if(_currentGroundCheckTick >= groundCheckTick)
                {
                    if (behaviour.Movement.Gravity.IsGrounded)
                    {
                        EndState();

                        return nextState;
                    }

                    _currentGroundCheckTick = 0;
                }
            }

            return this;
        }

        private void EndState()
        {
            behaviour.Movement.IsMoving = false;
            
            _isJumped = false;
            _currentDistanceFromPlayer = 0;
            _currentGroundCheckTick = 0;

            behaviour.Sequencer.NextSequence();
        }

        private void Jump()
        {
            if (!_isJumped)
            {
                behaviour.Movement.IsMoving = true;                
                behaviour.Movement.MoveRightNoFlipEnemy = !behaviour.Movement.MoveRightNoFlipEnemy;
                behaviour.Movement.Gravity.Jump(jumpForce);
                behaviour.ExclusiveAnimator.CallAnimationTrigger("jump");

                _isJumped = true;
            }
        }

        #region Editor Function
    #if UNITY_EDITOR
        private void OnDrawGizmosSelected() 
        {
            if(_showMinDistanceGizmos)
            {
                Gizmos.color = Color.red;

                Gizmos.DrawWireSphere(transform.position, minDistanceFromPlayer);
            }
        }
    #endif
        #endregion
    }
}
