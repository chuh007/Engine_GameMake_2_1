using _01Scripts.Core;
using _01Scripts.Entities;
using UnityEngine;

namespace _01Scripts.Players.States.UIInputStates
{
    public class UIAttackSelectInputState : UIInputState
    {
        public UIAttackSelectInputState(Entity entity, int animationHash) : base(entity, animationHash)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _player.PlayerBattleInput.OnCancelOrESCKeyPressed += HandleCancelOrEscKeyPressed;
            _player.PlayerBattleInput.OnSelectKeyPressed += HandleSelectKeyPressed;
            PlayerUIInoutComponent.InputUIChanged(ControlUIType.UIAttackSelect);

        }

        private void HandleSelectKeyPressed()
        {
            
        }

        private void HandleCancelOrEscKeyPressed()
        {
            _player.ChangeState("UISELECT");
        }
        
        public override void Exit()
        {
            _player.PlayerBattleInput.OnCancelOrESCKeyPressed -= HandleCancelOrEscKeyPressed;
            _player.PlayerBattleInput.OnSelectKeyPressed -= HandleSelectKeyPressed;
            base.Exit();
        }

    }
}