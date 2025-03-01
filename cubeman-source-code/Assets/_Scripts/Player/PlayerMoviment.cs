using Cubeman.Character;
using System.Collections;
using UnityEngine;

namespace Cubeman.Player
{
    [RequireComponent(typeof(CharacterGravity))]
    public sealed class PlayerMoviment : MonoBehaviour
    {
        #region Encapsulation
        public PlayerCharacterGravity Gravity { get => gravity; }
        public CharacterController CharacterController { get => controller; }

        public string DashSFX { get => DASH_AUDIO_KEY; }
        public string JumpInSFX { get => JUMP_IN_AUDIO_KEY; }
        public string JumpOutSFX { get => JUMP_OUT_AUDIO_KEY; }
        public bool MoveRight { get => _moveRight; }

        internal Vector2 XVelocity { get => _xVelocity; }
        internal Transform GraphicsTransform { get => graphicsTransform; }
        #endregion

        [Header("Classes")]
        [SerializeField] private PlayerBehaviour behaviour;
        [SerializeField] private PlayerCharacterGravity gravity;

        [Space(12)]

        [SerializeField] private CharacterController controller;

        [Header("Settings")]
        [SerializeField] [Range(1f, 20f)] private float movementSpeed = 6f;
        [SerializeField] [Range(4f, 20f)] private float dashSpeed = 12f;
        [SerializeField] [Range(0.1f, 2f)] private float groundDashDuration = 0.26f;
        [SerializeField] [Range(0.1f, 2f)] private float airDashDuration = 0.46f;
        [SerializeField] [Range(0.1f, 4f)] private float dashCouldown = 2f;

        [Space(12)]
        
        [SerializeField] [Range(0.1f, 1f)] private float staggerDuration = 0.2f;
        [SerializeField] [Range(1f, 12f)] private float staggerForce = 2.6f;

        [Space(12)]

        [SerializeField] private Transform graphicsTransform;

        private const string DASH_AUDIO_KEY = "audio_dash";
        private const string JUMP_IN_AUDIO_KEY = "audio_jumpIn";
        private const string JUMP_OUT_AUDIO_KEY = "audio_jumpOut";

        private bool _blockMoviment;
        private bool _inAutomaticMove;

        private bool _airDash;
        private bool _moveRight;
        private bool _isDashing;
        private bool _dashReady;
        private bool _horizontalImpulse;
        private bool _enableInputsOnAutomaticMoveComplete;

        private float _maxDashDuration;
        private float _currentDashDuration;

        private float _automaticMoveDuration;
        private float _currentAutomaticMoveDuration;

        private float _currentImpulseDuration;

        private Vector2 _xVelocity;
        private Vector2 _finalVelocity;

        private void Start() => SetupStartMovimentSide();

        private void SetupStartMovimentSide()
        {
            _dashReady = true;
            _moveRight = true;
        }

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
            if (_blockMoviment) return;

            CorrectGraphicsSide();

            AutomaticMoviment();

            DashMoviment();

            CheckAirDash();
        }

        private void AutomaticMoviment()
        {
            if (_inAutomaticMove)
            {
                AutomaticMoveTimer();
            }
        }

        private void CheckAirDash()
        {
            if (_airDash && gravity.IsGrounded)
            {
                _airDash = false;
            }
        }

        private void DashMoviment()
        {
            if (_isDashing)
            {
                DashTimer();
            }
            else if (!_horizontalImpulse && !_inAutomaticMove)
            {
                _xVelocity = movementSpeed * behaviour.Input.HorizontalAxis * Vector2.right;
            }
        }

        public void BlockMoviment(bool block) 
        {
            if(block)
            {
                _xVelocity = Vector2.zero;
            }
            _blockMoviment = block;
        }

        private void CorrectGraphicsSide()
        {
            if(behaviour.Input.HorizontalAxis > 0)
            {
                FlipGraphics(false);
            }
            else if(behaviour.Input.HorizontalAxis < 0)
            {
                FlipGraphics(true);
            }
        }

        private void FlipGraphics(bool flip)
        {
            if(flip)
            {
                var newScale = new Vector3(-1f, graphicsTransform.localScale.y, graphicsTransform.localScale.z);
                if (graphicsTransform.localScale != newScale)
                {
                    _moveRight = false;
                    graphicsTransform.localScale = newScale;
                }
            }
            else
            {
                var newScale = new Vector3(1f, graphicsTransform.localScale.y, graphicsTransform.localScale.z);
                if (graphicsTransform.localScale != newScale)
                {
                    _moveRight = true;
                    graphicsTransform.localScale = newScale;
                }
            }
        }

        public bool GetCurrentGraphicsFlippedSide()
        {
            if (graphicsTransform.localScale.x <= -1f)
            {
                return false;
            }

            return true;
        }

        internal void DashInput()
        {
            if(_dashReady && !_isDashing)
            {
                if (_airDash) return;

                SetupDashDistance();

                _isDashing = true;

                gravity.FreezeGravity = true;

                behaviour.Animation.DashAnimation = _isDashing;

                StartCoroutine(DashCouldownCoroutine());
            }
        }

        private void SetupDashDistance()
        {
            if (gravity.IsGrounded)
            {
                _airDash = false;
                _maxDashDuration = groundDashDuration;
            }
            else
            {
                _airDash = true;
                _maxDashDuration = airDashDuration;
            }
        }

        private void DashTimer()
        {
            _currentDashDuration += Time.deltaTime;
            if (_currentDashDuration >= _maxDashDuration)
            {
                gravity.FreezeGravity = false;

                _isDashing = false;
                _currentDashDuration = 0;

                behaviour.Animation.DashAnimation = _isDashing;
            }

            HorizontalForce(dashSpeed);
        }

        IEnumerator DashCouldownCoroutine()
        {
            _dashReady = false;

            yield return new WaitForSeconds(dashCouldown);

            _dashReady = true;
        }

        public void StartAutomaticMove(bool moveSide, bool enableInputsOnComplete,float duration)
        {
            behaviour.Input.GameplayInputs(false);
            _automaticMoveDuration = duration;
            _enableInputsOnAutomaticMoveComplete = enableInputsOnComplete;

            FlipGraphics(!moveSide);

            behaviour.Animation.AutomaticMoveAnimation = true;
            _inAutomaticMove = true;
        }

        private void AutomaticMoveTimer()
        {
            _currentAutomaticMoveDuration += Time.deltaTime;
            if (_currentAutomaticMoveDuration >= _automaticMoveDuration)
            {
                _inAutomaticMove = false;

                behaviour.Animation.AutomaticMoveAnimation = false;
                behaviour.Input.GameplayInputs(_enableInputsOnAutomaticMoveComplete);

                _currentAutomaticMoveDuration = 0;
            }

            HorizontalForce(movementSpeed);
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
            behaviour.BlockAction(true);

            _horizontalImpulse = true;

            while (_currentImpulseDuration < staggerDuration)
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
            behaviour.BlockAction(false);
        }
    }
}
