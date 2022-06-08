using UnityEngine;

namespace Cubeman.Enemies 
{
    public class ChasingState : State
    {
        #region Encapsulation
        internal float AttackingRange { get => attackingRange; }
        #endregion

        [Header("Classes")]
        [SerializeField] private EnemyBusterRobotBehaviour behaviour;

        [Header("Settings")]
        [SerializeField] private State attackingState;
        [SerializeField] private State backToOriginState;

        [Space(12)]

        [SerializeField] [Range(4f, 12f)] private float attackingRange = 4f;

        [Space(12)]

        [SerializeField] private Transform originPointTransform;
        [SerializeField] [Range(20f, 60f)] private float maxOriginDistance = 26f;

        private bool _movimentStarted;

        private float _distanceFromPlayer;
        private float _distanceFromOriginPoint;

        #region Editor Variable
#if UNITY_EDITOR
        [SerializeField] private bool showShootingGizmos;
        [SerializeField] private bool showOriginPointGizmos;
#endif
        #endregion

        public override State RunCurrentState()
        {
            MovimentStarted();
            MovimentSide();

            _distanceFromPlayer = behaviour.CheckPlayerDistance.PlayerDistance();

            if (_distanceFromPlayer <= attackingRange)
            {
                behaviour.Moviment.IsMoving = false;

                ResetState();

                return attackingState;
            }

            _distanceFromOriginPoint = CalculateDistanceX(behaviour.transform.position.x, 
                originPointTransform.position.x);

            if (_distanceFromOriginPoint > maxOriginDistance)
            {
                ResetState();

                return backToOriginState;
            }

            return this;
        }

        private void MovimentSide()
        {
            behaviour.Moviment.MoveRight = behaviour.PlayerSide.IsInRightSide();
        }

        private void MovimentStarted()
        {
            if (!_movimentStarted)
            {
                behaviour.Animator.RunningAnimation = true;

                behaviour.Moviment.IsMoving = true;

                _movimentStarted = true;
            }
        }

        private float CalculateDistanceX(float firstPosistion, float secondPosistion)
        {
            return Mathf.Abs(firstPosistion - secondPosistion);
        }

        private void OnDisable() => ResetState();

        private void ResetState()
        {
            ResetDistanceCounters();
            ResetBooleans();
        }

        private void ResetDistanceCounters()
        {
            _distanceFromPlayer = 0;
            _distanceFromOriginPoint = 0;
        }

        private void ResetBooleans()
        {
            _movimentStarted = false;
        }

        #region Editor
#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            if (showShootingGizmos)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(behaviour.transform.position, attackingRange);
            }

            if (showOriginPointGizmos)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(originPointTransform.position, 0.4f);
                Gizmos.DrawWireSphere(originPointTransform.position, maxOriginDistance);
            }
        }
#endif
        #endregion
    }
}