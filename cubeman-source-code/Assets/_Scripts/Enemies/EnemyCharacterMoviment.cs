using Cubeman.Character;
using UnityEngine;

namespace Cubeman.Enemies
{
    [RequireComponent(typeof(CharacterGravity))]
    public sealed class EnemyCharacterMoviment : MonoBehaviour
    {
        #region Encapsulation
        public bool MoveRight 
        {
            get => moveRight;
            set 
            {
                moveRight = value;
                FlipEnemy();
            } 
        }

        internal bool IsMoving { set => _isMoving = value; }
        #endregion

        [Header("Classes")]
        [SerializeField] private CharacterController charController;
        [SerializeField] private CharacterGravity gravity;

        [Header("Settings")]
        [SerializeField] private Transform graphicsTransform;

        [Space(12)]

        [SerializeField] [Range(1f, 12f)] private float movimentSpeed = 6f;
        [SerializeField] private bool moveRight;
        private bool _startSide;
        private bool _isMoving;

        private Vector2 _xVelocity;
        private Vector2 _finalVelocity;

        private void Awake() => _startSide = moveRight;

        private void OnEnable() => MoveRight = _startSide;

        private void Update() => CalculateMoviment();

        private void CalculateMoviment()
        {
            MoveEnemy();
            gravity.Gravity();

            _finalVelocity = _xVelocity + gravity.YVelocity;

            charController.Move(_finalVelocity * Time.deltaTime);
        }

        private void MoveEnemy()
        {
            if(_isMoving)
            {
                if (moveRight)
                {
                    _xVelocity = movimentSpeed * Vector2.right;
                }
                else
                {
                    _xVelocity = movimentSpeed * Vector2.left;
                }
            }
            else { _xVelocity = Vector2.zero; }
        }

        private void FlipEnemy()
        {
            Vector3 newScale = Vector3.zero;
            if (moveRight)
            {
                newScale = new Vector3(1f, graphicsTransform.localScale.y, graphicsTransform.localScale.z);
                if(graphicsTransform.localScale != newScale)
                {
                    graphicsTransform.localScale = newScale;
                }
            }
            else
            {
                newScale = new Vector3(-1f, graphicsTransform.localScale.y, graphicsTransform.localScale.z);
                if (graphicsTransform.localScale != newScale)
                {
                    graphicsTransform.localScale = newScale;
                }
            }
        }

        private void FlipEnemy(bool right)
        {
            if(right)
            {
                graphicsTransform.localScale = new Vector3(1f, graphicsTransform.localScale.y, graphicsTransform.localScale.z);
            }
            else
            {
                graphicsTransform.localScale = new Vector3(-1f, graphicsTransform.localScale.y, graphicsTransform.localScale.z);
            }
        }
    }
}
