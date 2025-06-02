using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _01Scripts.Players
{
    [CreateAssetMenu(fileName = "PlayerBattleInputSO", menuName = "SO/PlayerBattleInputSO", order = 0)]
    public class PlayerBattleInputSO : ScriptableObject, Controls.IBattlePlayerActions
    {
        public event Action OnAttackPressed;

        private Controls _controls;

        private void OnEnable()
        {
            if(_controls == null)
            {
                _controls = new Controls();
            }
            _controls.Enable();
            
        }
        
        private void OnDisable()
        {
            _controls.Player.Disable();
            _controls.BattlePlayer.Disable();
            _controls.UI.Disable();
        }
        
        public void SetCallbacks()
        {
            _controls.BattlePlayer.SetCallbacks(this);
        }
        
        public void OnAttackQTE(InputAction.CallbackContext context)
        {
            
        }

        public void OnEvasion(InputAction.CallbackContext context)
        {
            
        }

        public void OnSelect(InputAction.CallbackContext context)
        {
            
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            
        }
    }
}