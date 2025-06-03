using _01Scripts.Core;
using _01Scripts.Entities;
using UnityEngine;

namespace _01Scripts.Players.States.UIInputStates
{
    public class UIChoseTargetInputState : UIInputState
    {
        public UIChoseTargetInputState(Entity entity, int animationHash) : base(entity, animationHash)
        {
        }
        
        public override void Enter()
        {
            base.Enter();
            _player.PlayerBattleInput.OnCancelOrESCKeyPressed += HandleCancelOrEscKeyPressed;
            PlayerUIInoutComponent.InputUIChanged(ControlUIType.UIChoseTarget);
        }

        private void HandleCancelOrEscKeyPressed()
        {
            _player.ChangeState("UIATTACKSELECT");
        }

        public override void Exit()
        {
            _player.PlayerBattleInput.OnCancelOrESCKeyPressed -= HandleCancelOrEscKeyPressed;
            base.Exit();
        }
    }
}