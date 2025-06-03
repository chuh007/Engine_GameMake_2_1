﻿using _01Scripts.Core;
using _01Scripts.Entities;
using _01Scripts.FSM;
using UnityEngine;

namespace _01Scripts.Players.States.UIInputStates
{
    public class UISelectInputState : UIInputState
    {
        protected Player _player;
        public UISelectInputState(Entity entity, int animationHash) : base(entity, animationHash)
        {
            _player = entity as Player;
        }

        public override void Enter()
        {
            base.Enter();
            _player.PlayerBattleInput.OnCancelOrESCKeyPressed += HandleCancelOrEscKeyPressed;
            _player.PlayerBattleInput.OnAttackKeyPressed += HandleAttackKeyPressed;
            _player.PlayerBattleInput.OnItemKeyPressed += HandleItemPressed;
            PlayerUIInoutComponent.InputUIChanged(ControlUIType.UISelect);
        }
        
        private void HandleCancelOrEscKeyPressed()
        {
            // ESC 출력
        }

        private void HandleAttackKeyPressed()
        {
            _player.ChangeState("UIATTACKSELECT");
        }
        
        
        private void HandleItemPressed()
        {
            _player.ChangeState("UIUSEITEMSELECT");
        }

        
        
        public override void Exit()
        {
            _player.PlayerBattleInput.OnCancelOrESCKeyPressed -= HandleCancelOrEscKeyPressed;
            _player.PlayerBattleInput.OnAttackKeyPressed -= HandleAttackKeyPressed;
            _player.PlayerBattleInput.OnItemKeyPressed -= HandleItemPressed;
            base.Exit();
        }

    }
}