using _01Scripts.Combat;
using _01Scripts.Core;
using _01Scripts.Entities;
using UnityEngine;

namespace _01Scripts.Players.States.UIInputStates
{
    public class UIChoseTargetInputState : UIInputState
    {
        private PlayerAttackCompo _attackCompo;
        private PlayerTargetSelector _targetSelector;
        
        public UIChoseTargetInputState(Entity entity, int animationHash) : base(entity, animationHash)
        {
            _attackCompo = entity.GetCompo<PlayerAttackCompo>();
            _targetSelector = entity.GetCompo<PlayerTargetSelector>();
        }
        
        public override void Enter()
        {
            base.Enter();
            _player.PlayerBattleInput.OnCancelOrESCKeyPressed += HandleCancelOrEscKeyPressed;
            _player.PlayerBattleInput.OnSelectKeyPressed += HandleSelectKeyPressed;
            PlayerUIInoutComponent.InputUIChanged(ControlUIType.UIChoseTarget);
        }

        
        private void HandleSelectKeyPressed()
        {
            Entity target = _targetSelector.CurrentTarget;
            _attackCompo.SetTarget(target);
            _player.ChangeState("UIQTEINPUT");
        }
        
        private void HandleCancelOrEscKeyPressed()
        {
            _player.ChangeState("UIATTACKSELECT");
        }

        public override void Exit()
        {
            _player.PlayerBattleInput.OnCancelOrESCKeyPressed -= HandleCancelOrEscKeyPressed;
            _player.PlayerBattleInput.OnSelectKeyPressed -= HandleSelectKeyPressed;
            base.Exit();
        }
    }
}