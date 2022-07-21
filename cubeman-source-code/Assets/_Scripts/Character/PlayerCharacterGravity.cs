using UnityEngine;

namespace Cubeman.Character
{
    [RequireComponent(typeof(CharacterController))]
    public sealed class PlayerCharacterGravity : CharacterGravity
    {
        [Header("Exclusive Settings")]

        [SerializeField] [Range(0.1f, 1f)] private float variableJumpDuration = 0.3f;
        [SerializeField] [Range(0.1f, 4f)] private float variableJumpTimeToPeak = 0.3f;

        private bool _inVariableJump;
        private bool _groundVelocityAdjusted;
        private float _currentVariableJumpDuration;

        protected override void InitializeGravity()
        {
            CalculateGravity(maxHeight, timeToPeak);

            _jumpSpeed = _gravity * timeToPeak;
            _yVelocity = Vector2.down;
            _freezeGravity = false;
            _isJumped = false;

            _currentVariableJumpDuration = 0;
            _inVariableJump = false;
        }

        private void Update() => VariableJump();

        public override void Gravity()
        {
            if (!_freezeGravity)
            {
                if (!_groundVelocityAdjusted && IsGrounded && _yVelocity.y <= 0f)
                {
                    EndVariableJump();
                    _isJumped = false;
                    _yVelocity = Vector3.down;

                    _groundVelocityAdjusted = true;
                }
                else if (!IsGrounded)
                {
                    _yVelocity += _gravity * Time.deltaTime * Vector2.down;

                    _groundVelocityAdjusted = false;
                }
            }
        }

        public void VariableJumpInput()
        {
            if (_blockJump) return;

            if (!_freezeGravity && !_isJumped && !_inVariableJump)
            {
                CalculateGravity(variableJumpTimeToPeak);

                _isJumped = true;
                _inVariableJump = true;
            }
        }

        private void VariableJump()
        {
            if (_blockJump) return;

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
    }
}