using System;
using UnityEngine.InputSystem;
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

        private Action OnInteract;
        private Action OnSubmit;
        private Action OnCancel;

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

        public void SubscribeInteractInput(Action interactAction)
        {
            OnInteract = interactAction;
            _inputActions.Gameplay.Interact.started += Interact;
        }

        public void UnSubscribeInteractInput()
        {
            _inputActions.Gameplay.Interact.started -= Interact;
        }

        private void Interact(InputAction.CallbackContext ctx)
        {
            OnInteract();
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

            _inputActions.Gameplay.Interact.started -= Interact;
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
            OnSubmit = action;
            _inputActions.UI.Submit.started += Submit;
        }

        public void UnSubscribeSubmitInput()
        {
            _inputActions.UI.Submit.started -= Submit;
        }

        private void Submit(InputAction.CallbackContext ctx)
        {
            OnSubmit();
        }

        public void SubscribeCancelInput(Action action)
        {
            OnCancel = action;
            _inputActions.UI.Cancel.started += Cancel;
        }

        public void UnSubscribeCancelInput(Action action)
        {
            _inputActions.UI.Cancel.started -= Cancel;
        }

        private void Cancel(InputAction.CallbackContext ctx)
        {
            OnCancel();
        }

        private void DisableUIEvents()
        {
            _inputActions.UI.Submit.started -= Submit;
            _inputActions.UI.Cancel.started -= Cancel;
        }
    }
}
