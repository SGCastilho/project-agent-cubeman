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

        [Header("Settings")]
        [SerializeField] private bool disableInputsOnAwake;

        private GameplayInputActions _inputActions;

        private Action OnPause;
        private Action OnSubmit;
        private Action OnCancel;
        private Action OnUnPause;
        private Action OnInteract;

        private float _horizontalAxis;

        private void Awake() => SetupInputAction();

        private void SetupInputAction()
        {
            _inputActions = new GameplayInputActions();
        }

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
            if(disableInputsOnAwake)
            {
                UIInputs(false);
                GameplayInputs(false);
            }
            else
            {
                UIInputs(false);
                GameplayInputs(true);
            }
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

        public void SubscribePauseInput(Action pauseAction)
        {
            OnPause = pauseAction;
            _inputActions.Gameplay.Pause.started += Pause;
        }

        public void UnSubscribePauseInput()
        {
            _inputActions.Gameplay.Pause.started -= Pause;
        }

        private void Pause(InputAction.CallbackContext ctx)
        {
            OnPause();
        }

        public void PauseEnd()
        {
            GameplayInputs(false);
            UIInputs(true);

            behaviour.CursorState(false);
        }

        private void EnableGameplayEvents()
        {
            if (behaviour == null) return;

            _inputActions.Gameplay.Dash.started += ctx => behaviour.Moviment.DashInput();

            _inputActions.Gameplay.Jump.started += ctx => behaviour.Moviment.Gravity.VariableJumpInput();
            _inputActions.Gameplay.Jump.canceled += ctx => behaviour.Moviment.Gravity.EndVariableJump();

            _inputActions.Gameplay.Shoot.started += ctx => behaviour.Shoot.ShootingInput();
            _inputActions.Gameplay.Shoot.canceled += ctx => behaviour.Shoot.EndShooting();

            _inputActions.Gameplay.Ultimate.started += ctx => behaviour.Shoot.UltimateShooting();
        }

        private void DisableGameplayEvents()
        {
            if (behaviour == null) return;

            _inputActions.Gameplay.Dash.started -= ctx => behaviour.Moviment.DashInput();

            _inputActions.Gameplay.Jump.started -= ctx => behaviour.Moviment.Gravity.VariableJumpInput();
            _inputActions.Gameplay.Jump.canceled -= ctx => behaviour.Moviment.Gravity.EndVariableJump();

            _inputActions.Gameplay.Shoot.started -= ctx => behaviour.Shoot.ShootingInput();
            _inputActions.Gameplay.Shoot.canceled -= ctx => behaviour.Shoot.EndShooting();

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

        public void UnSubscribeCancelInput()
        {
            _inputActions.UI.Cancel.started -= Cancel;
        }

        private void Cancel(InputAction.CallbackContext ctx)
        {
            OnCancel();
        }

        public void SubscribeUnPauseInput(Action unPauseAction)
        {
            OnUnPause = unPauseAction;
            _inputActions.UI.UnPause.started += UnPause;
        }

        public void UnSubscribeUnPauseInput()
        {
            _inputActions.UI.UnPause.started -= UnPause;
        }

        private void UnPause(InputAction.CallbackContext ctx)
        {
            OnUnPause();
        }

        public void UnPauseEnd()
        {
            GameplayInputs(true);
            UIInputs(false);

            behaviour.CursorState(true);
        }

        private void DisableUIEvents()
        {
            _inputActions.UI.Submit.started -= Submit;
            _inputActions.UI.Cancel.started -= Cancel;
        }
    }
}
