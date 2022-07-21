using UnityEngine;

namespace Cubeman.Character
{
    [RequireComponent(typeof(CharacterController))]
    public class CharacterGravity : MonoBehaviour
    {
        #region Encapsulation
        public bool BlockJump { set => _blockJump = value; }

        public bool FreezeGravity
        {
            set
            {
                if (value == true)
                {
                    _yVelocity = Vector2.zero;
                }
                _freezeGravity = value;
            }
        }

        public bool IsGrounded { get => GroundCheck(); }
        public bool IsJumped { get => _isJumped; }
        public Vector2 YVelocity { get => _yVelocity; }
        #endregion

        [Header("Classes")]
        [SerializeField] protected CharacterController charController;

        [Header("Settings")]
        [SerializeField] [Range(0.1f, 12f)] protected float maxHeight = 1f;
        [SerializeField] [Range(0.1f, 4f)] protected float timeToPeak = 0.2f;

        protected bool _blockJump;
        protected bool _freezeGravity;

        protected bool _isJumped;
        protected float _gravity;
        protected float _jumpSpeed;

        protected Vector2 _yVelocity;
        
        [Space(12)]

        [SerializeField] protected Transform groundCheckTransform;
        [SerializeField] [Range(0.1f, 2f)] protected float groundCheckRange = 0.28f;
        [SerializeField] protected LayerMask groundLayerMask;

        protected void Start() => InitializeGravity();

        protected virtual void InitializeGravity()
        {
            CalculateGravity(maxHeight, timeToPeak);

            _jumpSpeed = _gravity * timeToPeak;
            _freezeGravity = false;
            _isJumped = false;
        }

        public virtual void Gravity()
        {
            if (!_freezeGravity)
            {
                if (IsGrounded && _yVelocity.y <= 0f)
                {
                    _isJumped = false;
                    _yVelocity = Vector3.down;
                }
                else if(!IsGrounded)
                {
                    _yVelocity += _gravity * Time.deltaTime * Vector2.down;
                }
            }
        }

        public void CalculateGravity(float timeToPeak)
        {
            _gravity = (2 * maxHeight) / Mathf.Pow(timeToPeak, 2);
        }

        public void CalculateGravity(float maxHeight, float timeToPeak)
        {
            _gravity = (2 * maxHeight) / Mathf.Pow(timeToPeak, 2);
        }

        protected bool GroundCheck()
        {
            return Physics.CheckSphere(groundCheckTransform.position, groundCheckRange, groundLayerMask);
        }

        public void Jump()
        {
            if (_blockJump) return;

            if (!_freezeGravity && IsGrounded)
            {
                _yVelocity = _jumpSpeed * Vector2.up;
                _isJumped = true;
            }
        }

        public void Jump(float force)
        {
            if (_blockJump) return;

            if (!_freezeGravity && IsGrounded)
            {
                _yVelocity = force * Vector2.up;
                _isJumped = true;
            }
        }

        #region Editor
#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            if (groundCheckTransform != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(groundCheckTransform.position, groundCheckRange);
            }
        }
#endif
        #endregion
    }
}