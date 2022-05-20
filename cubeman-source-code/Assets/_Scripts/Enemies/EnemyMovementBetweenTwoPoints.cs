using UnityEngine;

namespace Cubeman.Enemies
{
    public class EnemyMovementBetweenTwoPoints : MonoBehaviour
    {
        #region Encapsulation
        internal bool StarMoveRight { get => startMovingRight; }
        internal bool IsMoving 
        {
            set
            {
                if(value == false)
                {
                    rigidBody.velocity = Vector3.zero;
                }

                _isMoving = value;
            }
        }

        internal Transform EnemyTransform { get => _transform; }
        #endregion

        [Header("Classes")]
        [SerializeField] protected Rigidbody rigidBody;

        [Header("Settings")]
        [SerializeField] protected Transform directionPointA;
        [SerializeField] protected Transform directionPointB;
        protected float _distanceFromPoint;

        [Space(12)]

        [SerializeField] [Range(2f, 12f)] protected float movimentSpeed = 6f;
        [SerializeField] protected bool startMovingRight;
        protected bool _moveRight;
        protected bool _isMoving;

        protected Vector3 _startPosistion;
        protected Transform _transform;

        protected void Awake()
        {
            _transform = transform;
            _startPosistion = transform.localPosition;
        }

        protected virtual void OnEnable()
        {
            _moveRight = startMovingRight;
            _isMoving = true;
        }

        protected void OnDisable()
        {
            _transform.localPosition = _startPosistion;
        }

        protected void Update() => InvertMoviment();

        protected virtual void InvertMoviment()
        {
            if (_isMoving && _moveRight)
            {
                _distanceFromPoint = CalculateDistance(_transform.position.x, directionPointB.position.x);
                if (_distanceFromPoint < 0.1f)
                {
                    _moveRight = !_moveRight;
                }
            }
            else
            {
                _distanceFromPoint = CalculateDistance(_transform.position.x, directionPointA.position.x);
                if (_distanceFromPoint < 0.1f)
                {
                    _moveRight = !_moveRight;
                }
            }
        }

        protected float CalculateDistance(float pointA, float pointB)
        {
            return Mathf.Abs(pointA - pointB);
        }

        protected void FixedUpdate() => Moviment();

        protected void Moviment()
        {
            if (_isMoving)
            {
                if (_moveRight)
                {
                    rigidBody.velocity = movimentSpeed * Vector2.right;
                }
                else
                {
                    rigidBody.velocity = movimentSpeed * Vector2.left;
                }
            }
        }

        #region Editor
#if UNITY_EDITOR
        protected void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;

            Gizmos.DrawSphere(directionPointA.position, 0.2f);
            Gizmos.DrawSphere(directionPointB.position, 0.2f);

            Gizmos.color = Color.red;

            Gizmos.DrawLine(directionPointA.position, directionPointB.position);
        }
#endif
        #endregion
    }
}
