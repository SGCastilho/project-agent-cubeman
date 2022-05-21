using UnityEngine;

namespace Cubeman.Player
{
    public sealed class PlayerInput : MonoBehaviour
    {
        #region Encapsulation
        internal float HorizontalAxis { get => _horizontalAxis; }
        #endregion

        [Header("Classes")]
        [SerializeField] private PlayerBehaviour behaviour;

        private GameplayInputActions _inputActions;

        private float _horizontalAxis;

        private void Awake() => _inputActions = new GameplayInputActions();

        private void OnEnable()
        {
            _inputActions.Enable();
            EnableGameplayEvents();
        }

        private void OnDisable()
        {
            _inputActions.Disable();
            DisableGameplayEvents();
        }

        private void Start() => GameplayInputs(false);

        private void Update() => ConstantInputs();

        private void ConstantInputs()
        {
            _horizontalAxis = _inputActions.Gameplay.Horizontal.ReadValue<float>();
        }

        public void GameplayInputs(bool enable)
        {
            if(enable)
            {
                _inputActions.Gameplay.Enable();
            }
            else
            {
                _inputActions.Gameplay.Disable();
            }
        }

        private void EnableGameplayEvents()
        {
            _inputActions.Gameplay.Dash.started += ctx => behaviour.Moviment.DashInput();

            _inputActions.Gameplay.Jump.started += ctx => behaviour.Moviment.Gravity.VariableJumpInput();
            _inputActions.Gameplay.Jump.canceled += ctx => behaviour.Moviment.Gravity.EndVariableJump();

            _inputActions.Gameplay.Shoot.started += ctx => behaviour.Shoot.Shooting();
            _inputActions.Gameplay.Shoot.performed += ctx => behaviour.Shoot.Shooting();

            _inputActions.Gameplay.Ultimate.started += ctx => behaviour.Shoot.UltimateShooting();
        }

        private void DisableGameplayEvents()
        {
            _inputActions.Gameplay.Dash.started -= ctx => behaviour.Moviment.DashInput();

            _inputActions.Gameplay.Jump.started -= ctx => behaviour.Moviment.Gravity.VariableJumpInput();
            _inputActions.Gameplay.Jump.canceled -= ctx => behaviour.Moviment.Gravity.EndVariableJump();

            _inputActions.Gameplay.Shoot.started -= ctx => behaviour.Shoot.Shooting();
            _inputActions.Gameplay.Shoot.performed -= ctx => behaviour.Shoot.Shooting();

            _inputActions.Gameplay.Ultimate.started -= ctx => behaviour.Shoot.UltimateShooting();
        }
    }
}
