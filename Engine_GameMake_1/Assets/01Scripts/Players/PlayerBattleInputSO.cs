using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _01Scripts.Players
{
    [CreateAssetMenu(fileName = "PlayerBattleInputSO", menuName = "SO/PlayerBattleInputSO", order = 0)]
    public class PlayerBattleInputSO : ScriptableObject, Controls.IBattlePlayerActions
    {
        public event Action OnAttackKeyPressed;
        public event Action OnQTEKeyPressed;
        public event Action OnBlockKeyPressed;
        public event Action OnItemKeyPressed;
        public event Action OnCancelOrESCKeyPressed;
        public event Action OnSelectKeyPressed;

        private Controls _controls;

        private void OnEnable()
        {
            if(_controls == null)
            {
                _controls = new Controls();
                _controls.BattlePlayer.SetCallbacks(this);
            }
            _controls.Enable();
            
        }
        
        private void OnDisable()
        {
            _controls.Player.Disable();
            _controls.BattlePlayer.Disable();
            _controls.UI.Disable();
        }
        
        public void RemoveCallbacks()
        {
            _controls.BattlePlayer.Disable();
        }
        
        public void SetCallbacks()
        {
            _controls.BattlePlayer.Enable();
        }
        
        public void OnAttackQTE(InputAction.CallbackContext context)
        {
            if(context.performed)
                OnQTEKeyPressed?.Invoke();
        }

        public void OnBlock(InputAction.CallbackContext context)
        {
            if(context.performed)
                OnBlockKeyPressed?.Invoke();
        }

        public void OnSelect(InputAction.CallbackContext context)
        {
            if(context.performed)
                OnSelectKeyPressed?.Invoke();
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                OnAttackKeyPressed?.Invoke();
            }
        }
        
        public void OnItem(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnItemKeyPressed?.Invoke();
        }

        public void OnCancelOrESC(InputAction.CallbackContext context)
        {
            if(context.performed)
                OnCancelOrESCKeyPressed?.Invoke();
        }

        public void OnMouse(InputAction.CallbackContext context)
        {
            
        }

    }
}