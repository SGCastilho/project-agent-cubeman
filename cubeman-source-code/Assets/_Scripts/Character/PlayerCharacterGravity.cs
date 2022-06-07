using UnityEngine;

namespace Cubeman.Character
{
    [RequireComponent(typeof(CharacterController))]
    public sealed class PlayerCharacterGravity : CharacterGravity
    {
        [Header("Exclusive Settings")]

        [SerializeField] [Range(0.1f, 1f)] private float variableJumpDuration = 0.3f;
        [SerializeField] [Range(0.1f, 4f)] private float variableJumpTimeToPeak = 0.3f;
        private float _currentVariableJumpDuration;
        private bool _inVariableJump;

        protected override void InitializeGravity()
        {
            base.InitializeGravity();

            _currentVariableJumpDuration = 0;
            _inVariableJump = false;
        }

        private void Update() => VariableJump();

        public override void Gravity()
        {
            if (!_freezeGravity)
            {
                if (charController.isGrounded && _yVelocity.y < -1f)
                {
                    EndVariableJump();
                    _isJumped = false;
                    _yVelocity = Vector3.down;
                }
                else if (!charController.isGrounded)
                {
                    _yVelocity += _gravity * Time.deltaTime * Vector2.down;
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