using UnityEngine;

namespace Cubeman.Enemies 
{
    public class BackToOriginState : State
    {
        [Header("Classes")]
        [SerializeField] private EnemyBusterRobotBehaviour behaviour;

        [Header("Settings")]
        [SerializeField] private State nextState;

        [Space(12)]

        [SerializeField] private Transform originPointTransform;

        private bool _backToOriginStarted;

        private float _distanceFromOriginPoint;

        public override State RunCurrentState()
        {
            BackToOriginStart();

            _distanceFromOriginPoint = CalculateDistanceX(behaviour.transform.position.x, 
                originPointTransform.position.x);

            if (_distanceFromOriginPoint < 0.1f)
            {
                behaviour.Animator.RunningAnimation = false;
                behaviour.Moviment.IsMoving = false;

                ResetState();

                return nextState;
            }

            return this;
        }

        private void BackToOriginStart()
        {
            if (!_backToOriginStarted)
            {
                behaviour.Moviment.MoveRight = CheckObjectSide(behaviour.transform.position.x,
                    originPointTransform.position.x);

                behaviour.Moviment.IsMoving = true;

                _backToOriginStarted = true;
            }
        }

        private bool CheckObjectSide(float firstPosistion, float secondPosistion)
        {
            if (firstPosistion > secondPosistion)
            {
                return false;
            }

            return true;
        }

        private float CalculateDistanceX(float firstPosistion, float secondPosistion)
        {
            return Mathf.Abs(firstPosistion - secondPosistion);
        }

        private void OnDisable() => ResetState();

        private void ResetState()
        {
            ResetBooleans();
            ResetDistanceCounter();
        }

        private void ResetBooleans()
        {
            _backToOriginStarted = false;
        }

        private void ResetDistanceCounter()
        {
            _distanceFromOriginPoint = 0;
        }
    }
}