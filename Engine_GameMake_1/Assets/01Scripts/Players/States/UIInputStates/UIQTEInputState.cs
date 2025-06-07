using _01Scripts.Combat;
using _01Scripts.Core;
using _01Scripts.Entities;
using QTESystem;
using UnityEngine;

namespace _01Scripts.Players.States.UIInputStates
{
    public class UIQTEInputState : UIInputState
    {
        private SlidingQTE _qteCompo;
        
        public UIQTEInputState(Entity entity, int animationHash) : base(entity, animationHash)
        {
            _qteCompo = _player.GetCompo<SlidingQTE>();
        }

        public override void Enter()
        {
            base.Enter();
            PlayerUIInoutComponent.InputUIChanged(ControlUIType.UIQTEInput);
            _player.PlayerBattleInput.OnQTEKeyPressed += HandleQTEPressed;
            _qteCompo.onSuccess.AddListener(HandleSuccess);
            _qteCompo.onFailure.AddListener(HandleFailure);
            _qteCompo.BeginQTE();
        }

        private void HandleSuccess()
        {
            _player.ChangeState("ATTACKMOTION");
        }
        
        private void HandleFailure()
        {
            _player.ChangeState("ATTACKMOTION");
        }

        private void HandleQTEPressed()
        {
            // QTE 진행, QTE 끝나면 공격 컴포넌트에 공격 명령 -- 새 상태 만들었. 거따 전이.
            _qteCompo.HandleQTEPressed();
            // _player.ChangeState("ATTACKMOTION");
        }

        public override void Exit()
        {
            _player.PlayerBattleInput.OnQTEKeyPressed -= HandleQTEPressed;
            base.Exit();
        }
    }
}