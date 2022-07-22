using System;
using UnityEngine.InputSystem;
using UnityEngine;

namespace Cubeman.Manager
{
    public sealed class MainMenuInputs : MonoBehaviour
    {
        private MenusInputActions _inputActions;

        private Action OnAnyKeyPress;

        private void Awake()
        {
            _inputActions = new MenusInputActions();
        }

        private void OnEnable()
        {
            _inputActions.Enable();
        }

        private void OnDisable()
        {
            DisableEvents();

            _inputActions.Disable();
        }

        public void SubscribeAnyKeyInput(Action interactAction)
        {
            OnAnyKeyPress = interactAction;
            _inputActions.UI.AnyKey.started += AnyKeyPress;
        }

        public void UnSubscribeAnyKeyInput()
        {
            _inputActions.UI.AnyKey.started -= AnyKeyPress;
        }

        private void AnyKeyPress(InputAction.CallbackContext ctx)
        {
            OnAnyKeyPress();
        }

        private void DisableEvents()
        {
            UnSubscribeAnyKeyInput();
        }
    }
}
