using _01Scripts.Combat;
using _01Scripts.Core;
using _01Scripts.Entities;
using UnityEngine;

namespace _01Scripts.Players.States.UIInputStates
{
    public class UIQTEInputState : UIInputState
    {
        private PlayerAttackCompo _attackCompo;
        
        public UIQTEInputState(Entity entity, int animationHash) : base(entity, animationHash)
        {
            _attackCompo = entity.GetCompo<PlayerAttackCompo>();
        }

        public override void Enter()
        {
            base.Enter();
            PlayerUIInoutComponent.InputUIChanged(ControlUIType.UIQTEInput);
            _player.PlayerBattleInput.OnQTEKeyPressed += HandleQTEPressed;
        }

        private void HandleQTEPressed()
        {
            // QTE 진행, QTE 끝나면 공격 컴포넌트에 공격 명령
            _attackCompo.Attack();
        }

        public override void Exit()
        {
            _player.PlayerBattleInput.OnQTEKeyPressed += HandleQTEPressed;
            base.Exit();
        }
    }
}