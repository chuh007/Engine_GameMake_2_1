using _01Scripts.Combat;
using _01Scripts.Core;
using _01Scripts.Entities;
using UnityEngine;

namespace _01Scripts.Players.States.UIInputStates
{
    public class UIQTEInputState : UIInputState
    {
        
        public UIQTEInputState(Entity entity, int animationHash) : base(entity, animationHash)
        {
        }

        public override void Enter()
        {
            base.Enter();
            PlayerUIInoutComponent.InputUIChanged(ControlUIType.UIQTEInput);
            _player.PlayerBattleInput.OnQTEKeyPressed += HandleQTEPressed;
        }

        private void HandleQTEPressed()
        {
            // QTE 진행, QTE 끝나면 공격 컴포넌트에 공격 명령 -- 새 상태 만들었. 거따 전이.
            _player.ChangeState("ATTACKMOTION");
        }

        public override void Exit()
        {
            _player.PlayerBattleInput.OnQTEKeyPressed -= HandleQTEPressed;
            base.Exit();
        }
    }
}