using System;
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

        private Action _lastInteractAction;
        private Action _lastSubmitAction;
        private Action _lastCancelAction;

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
            DisableUIEvents();
            DisableGameplayEvents();
        }

        private void Start() => SetupInputs();

        private void SetupInputs()
        {
            UIInputs(false);
            GameplayInputs(false);
        }

        private void Update() => ConstantInputs();

        private void ConstantInputs()
        {
            _horizontalAxis = _inputActions.Gameplay.Horizontal.ReadValue<float>();
        }

        public void SubscribeInteractInput(Action action)
        {
            _lastInteractAction = action;
            _inputActions.Gameplay.Interact.started += ctx => action();
        }

        public void UnSubscribeInteractInput(Action action)
        {
            _inputActions.Gameplay.Interact.started -= ctx => action();
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

            _inputActions.Gameplay.Interact.started -= ctx => _lastInteractAction();
        }

        private void DisableUIEvents()
        {
            _inputActions.UI.Submit.started -= ctx => _lastSubmitAction();
            _inputActions.UI.Cancel.started -= ctx => _lastCancelAction();
        }

        public void UIInputs(bool enable)
        {
            if(enable)
            {
                _inputActions.UI.Enable();
            }
            else
            {
                _inputActions.UI.Disable();
            }
        }

        public void SubscribeSubmitInput(Action action)
        {
            _lastSubmitAction = action;
            _inputActions.UI.Submit.started += ctx => action();
        }

        public void UnSubscribeSubmitInput(Action action)
        {
            _inputActions.UI.Submit.started -= ctx => action();
        }

        public void SubscribeCancelInput(Action action)
        {
            _lastCancelAction = action;
            _inputActions.UI.Cancel.started += ctx => action();
        }

        public void UnSubscribeCancelInput(Action action)
        {
            _inputActions.UI.Cancel.started -= ctx => action();
        }
    }
}
