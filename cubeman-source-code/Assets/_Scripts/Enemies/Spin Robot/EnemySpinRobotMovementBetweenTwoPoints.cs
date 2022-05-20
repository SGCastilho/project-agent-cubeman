using Cubeman.Utilities;
using UnityEngine;

namespace Cubeman.Enemies
{
    public sealed class EnemySpinRobotMovementBetweenTwoPoints : EnemyMovementBetweenTwoPoints
    {
        [Header("Spin Robot Classes")]
        [SerializeField] private RotateObject rotate;

        protected override void OnEnable()
        {
            base.OnEnable();
            rotate.RotateRight = startMovingRight;
        }

        protected override void InvertMoviment()
        {
            if (_isMoving && _moveRight)
            {
                _distanceFromPoint = CalculateDistance(_transform.position.x, directionPointB.position.x);
                if (_distanceFromPoint < 0.1f)
                {
                    _moveRight = !_moveRight;
                    rotate.RotateRight = _moveRight;
                }
            }
            else
            {
                _distanceFromPoint = CalculateDistance(_transform.position.x, directionPointA.position.x);
                if (_distanceFromPoint < 0.1f)
                {
                    _moveRight = !_moveRight;
                    rotate.RotateRight = _moveRight;
                }
            }
        }
    }
}
