using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SurockGames.Input
{
    public class UnityInputSystemService
    {
        public Vector2 Axis
        {
            get
            {
                Vector2 axis = moveAction.ReadValue<Vector2>();
                return axis;
            }
        }
        public Vector2 LookAxis
        {
            get
            {
                Vector2 axis = lookAction.ReadValue<Vector2>();
                return axis;
            }
        }
        public bool IsJumping
        {
            get
            {
                return jumpAction.IsInProgress();
            }
        }
        public bool IsAiming
        {
            get
            {
                return aimAction.IsInProgress();
            }
        }

        private PlayerInput inputActions;

        private InputAction moveAction;
        private InputAction lookAction;
        private InputAction jumpAction;
        private InputAction specialAction;
        private InputAction interactAction;
        private InputAction throwAction;
        private InputAction aimAction;
        private InputAction changeToNextCharacterAction;
        private InputAction changeToPreviousCharacterAction;

        public event Action OnChangeToNextCharacterEvent;
        public event Action OnChangeToPreviousCharacterEvent;
        public event Action OnScecialActionEvent;
        public event Action OnInteractEvent;
        public event Action OnThrowEvent;
        public event Action OnAimEvent;
        public event Action OnStartAimEvent;

        public UnityInputSystemService()
        {
            inputActions = new PlayerInput();
            EnableBindings();
        }

        private void EnableBindings()
        {
            moveAction = inputActions.Character.Move;
            moveAction.Enable();

            lookAction = inputActions.Character.Look;
            lookAction.Enable();

            jumpAction = inputActions.Character.Jump;
            jumpAction.Enable();

            aimAction = inputActions.Character.Aim;
            aimAction.Enable();

            EnableBindingForActionPerformed(specialAction, inputActions.Character.SpecialAction, TriggerInteractEvent);
            EnableBindingForActionPerformed(interactAction, inputActions.Character.Interact, TriggerInteractEvent);
            EnableBindingForActionPerformed(throwAction, inputActions.Character.Throw, TriggerThrowEvent);
            EnableBindingForActionPerformed(changeToNextCharacterAction, inputActions.Character.ChangeToNextCharacter, TriggerChangeNextCharacterEvent);
            EnableBindingForActionPerformed(changeToPreviousCharacterAction, inputActions.Character.ChangeToPreviousCharacter, TriggerChangePreviousCharacterEvent);
        }

        private void EnableBindingForActionPerformed(InputAction inputAction, InputAction actionToBind, Action<InputAction.CallbackContext> actionToSign)
        {
            inputAction = actionToBind;
            inputAction.performed += actionToSign;
            inputAction.Enable();
        }

        protected void TriggerChangeNextCharacterEvent(InputAction.CallbackContext context)
        {
            OnChangeToNextCharacterEvent?.Invoke();
        }

        protected void TriggerChangePreviousCharacterEvent(InputAction.CallbackContext context)
        {
            OnChangeToPreviousCharacterEvent?.Invoke();
        }

        protected void TriggerSpecialActionEvent(InputAction.CallbackContext context)
        {
            OnChangeToPreviousCharacterEvent?.Invoke();
        }

        protected void TriggerInteractEvent(InputAction.CallbackContext context)
        {
            OnInteractEvent?.Invoke();
        }

        protected void TriggerThrowEvent(InputAction.CallbackContext context)
        {
            OnThrowEvent?.Invoke();
        }
        protected void TriggerStartAimEvent(InputAction.CallbackContext context)
        {
            OnAimEvent?.Invoke();
        }
    }
}