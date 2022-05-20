using System.Collections;
using Cubeman.Character;
using UnityEngine;

namespace Cubeman.Player
{
    [RequireComponent(typeof(CharacterGravity))]
    public sealed class PlayerMoviment : MonoBehaviour
    {
        #region Encapsulation
        public CharacterGravity Gravity { get => gravity; }

        public string DashSFX { get => DASH_AUDIO_KEY; }
        public string JumpInSFX { get => JUMP_IN_AUDIO_KEY; }
        public string JumpOutSFX { get => JUMP_OUT_AUDIO_KEY; }
        public bool MoveRight { get => _moveRight; }

        internal Vector2 XVelocity { get => _xVelocity; }
        internal Transform GraphicsTransform { get => graphicsTransform; }
        #endregion

        private const string DASH_AUDIO_KEY = "audio_dash";
        private const string JUMP_IN_AUDIO_KEY = "audio_jumpIn";
        private const string JUMP_OUT_AUDIO_KEY = "audio_jumpOut";

        [Header("Classes")]
        [SerializeField] private PlayerBehaviour behaviour;
        [Space(12)]
        [SerializeField] private CharacterController controller;
        [SerializeField] private CharacterGravity gravity;

        [Header("Settings")]
        [SerializeField] [Range(1f, 20f)] private float movementSpeed = 6f;
        [SerializeField] [Range(4f, 20f)] private float dashSpeed = 12f;
        [SerializeField] [Range(0.1f, 2f)] private float dashDuration = 0.26f;
        [Space(12)]
        [SerializeField] [Range(0.1f, 1f)] private float staggerDuration = 0.2f;
        [SerializeField] [Range(1f, 12f)] private float staggerForce = 2.6f;

        private bool _moveRight;
        private bool _isDashing;
        private bool _horizontalImpulse;

        private float _currentDashDuration;
        private float _currentImpulseDuration;

        private Vector2 _xVelocity;
        private Vector2 _finalVelocity;

        [Space(12)]

        [SerializeField] private Transform graphicsTransform;

        private void Start() => _moveRight = true;

        private void Update() => CalculateMoviment();

        private void CalculateMoviment()
        {
            HorizontalMoviment();
            gravity.Gravity();

            _finalVelocity = _xVelocity + gravity.YVelocity;

            controller.Move(_finalVelocity * Time.deltaTime);
        }

        private void HorizontalMoviment()
        {
            CorrectGraphicsSide();

            if(_isDashing) { DashTimer(); }
            else if(!_horizontalImpulse) 
            { _xVelocity = movementSpeed * behaviour.Input.HorizontalAxis * Vector2.right; }
        }

        private void CorrectGraphicsSide()
        {
            if(behaviour.Input.HorizontalAxis > 0)
            {
                var newScale = new Vector3(1f, graphicsTransform.localScale.y, graphicsTransform.localScale.z);
                if(graphicsTransform.localScale != newScale)
                {
                    _moveRight = true;
                    graphicsTransform.localScale = newScale;
                }
            }
            else if(behaviour.Input.HorizontalAxis < 0)
            {
                var newScale = new Vector3(-1f, graphicsTransform.localScale.y, graphicsTransform.localScale.z);
                if (graphicsTransform.localScale != newScale)
                {
                    _moveRight = false;
                    graphicsTransform.localScale = newScale;
                }
            }
        }

        internal void DashInput()
        {
            if(gravity.IsGrounded && !gravity.IsJumped && !_isDashing)
            {
                gravity.FreezeGravity = true;
                _isDashing = true;

                behaviour.Animation.DashAnimation = _isDashing;
            }
        }

        private void DashTimer()
        {
            _currentDashDuration += Time.deltaTime;
            if (_currentDashDuration >= dashDuration)
            {
                gravity.FreezeGravity = false;
                _isDashing = false;
                _currentDashDuration = 0;

                behaviour.Animation.DashAnimation = _isDashing;
            }

            HorizontalForce(dashSpeed);
        }

        private void HorizontalForce(float force)
        {
            if (_moveRight)
            {
                _xVelocity = force * Vector2.right;
            }
            else
            {
                _xVelocity = force * Vector2.left;
            }
        }

        private void HorizontalInverseForce(float force)
        {
            if (_moveRight)
            {
                _xVelocity = force * Vector2.left;
            }
            else
            {
                _xVelocity = force * Vector2.right;
            }
        }

        public IEnumerator TakeDamageImpulseCoroutine()
        {
            behaviour.Input.GameplayInputs(false);
            gravity.FreezeGravity = true;
            _horizontalImpulse = true;

            while(_currentImpulseDuration < staggerDuration)
            {
                _currentImpulseDuration += Time.deltaTime;
                HorizontalInverseForce(staggerForce);
                yield return null;
            }

            yield return new WaitForSeconds(0.1f);
            _currentImpulseDuration = 0;
            _horizontalImpulse = false;
            gravity.FreezeGravity = false;
            behaviour.Animation.TakeDamageAnimation = false;
            behaviour.Input.GameplayInputs(true);
        }
    }
}
