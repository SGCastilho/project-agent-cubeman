using UnityEngine;

namespace Cubeman.Character
{
    [RequireComponent(typeof(CharacterController))]
    public sealed class CharacterGravity : MonoBehaviour
    {
        #region Encapsulation
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

        [Header("Settings")]
        [SerializeField] [Range(0.1f, 12f)] private float maxHeight = 1f;
        [SerializeField] [Range(0.1f, 4f)] private float timeToPeak = 0.2f;
        private bool _freezeGravity;
        private bool _isJumped;
        private float _gravity;
        private float _jumpSpeed;
        private Vector2 _yVelocity;

        [Space(12)]

        [SerializeField] [Range(0.1f, 1f)] private float variableJumpDuration = 0.3f;
        [SerializeField] [Range(0.1f, 4f)] private float variableJumpTimeToPeak = 0.3f;
        private float _currentVariableJumpDuration;
        private bool _inVariableJump;

        [Space(12)]

        [SerializeField] private Transform groundCheckTransform;
        [SerializeField] [Range(0.1f, 2f)] private float groundCheckRange = 0.28f;
        [SerializeField] private LayerMask groundLayerMask;

        private void Start() => InitializeGravity();

        private void InitializeGravity()
        {
            CalculateGravity(maxHeight, timeToPeak);
            _jumpSpeed = _gravity * timeToPeak;
            _freezeGravity = false;
            _isJumped = false;

            _currentVariableJumpDuration = 0;
            _inVariableJump = false;
        }

        private void Update() => VariableJump();

        public void Gravity()
        {
            if (!_freezeGravity)
            {
                if (IsGrounded && _yVelocity.y < -1f)
                {
                    EndVariableJump();
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

        private bool GroundCheck()
        {
            return Physics.CheckSphere(groundCheckTransform.position, groundCheckRange, groundLayerMask);
        }

        public void Jump()
        {
            if (!_freezeGravity && GroundCheck())
            {
                _yVelocity = _jumpSpeed * Vector2.up;
                _isJumped = true;
            }
        }

        public void Jump(float force)
        {
            if (!_freezeGravity && GroundCheck())
            {
                _yVelocity = force * Vector2.up;
                _isJumped = true;
            }
        }

        public void VariableJumpInput()
        {
            if (!_freezeGravity && !_isJumped && !_inVariableJump)
            {
                CalculateGravity(variableJumpTimeToPeak);
                _isJumped = true;
                _inVariableJump = true;
            }
        }

        private void VariableJump()
        {
            if (!_freezeGravity && _inVariableJump)
            {
                _currentVariableJumpDuration += Time.deltaTime;
                _yVelocity = _jumpSpeed * Vector2.up;
                if (_currentVariableJumpDuration >= variableJumpDuration)
                {
                    _currentVariableJumpDuration = 0;
                    _inVariableJump = false;
                }
            }
        }

        public void EndVariableJump()
        {
            CalculateGravity(timeToPeak);
            _currentVariableJumpDuration = 0;
            _inVariableJump = false;
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