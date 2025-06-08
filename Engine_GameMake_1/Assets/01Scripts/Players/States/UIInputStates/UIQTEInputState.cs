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
        private PlayerAttackCompo _playerAttackCompo;

        private int _triggerdCount = 0;
        
        public UIQTEInputState(Entity entity, int animationHash) : base(entity, animationHash)
        {
            _qteCompo = _player.GetCompo<SlidingQTE>();
            _playerAttackCompo = _player.GetCompo<PlayerAttackCompo>();
        }

        public override void Enter()
        {
            base.Enter();
            PlayerUIInoutComponent.InputUIChanged(ControlUIType.UIQTEInput);
            _player.PlayerBattleInput.OnQTEKeyPressed += HandleQTEPressed;
            _qteCompo.onSuccess.AddListener(HandleSuccess);
            _qteCompo.onFailure.AddListener(HandleFailure);
            _triggerdCount = 0;
            _qteCompo.BeginQTE();
        }

        private void HandleSuccess()
        {
            _playerAttackCompo.QteSuccess();
            if (_triggerdCount < _playerAttackCompo.currentAttackData.triggerCount - 1)
            {
                _triggerdCount++;
                _qteCompo.BeginQTE();
                return;
            }
            _player.ChangeState("ATTACKMOTION");
        }
        
        private void HandleFailure()
        {
            _player.ChangeState("ATTACKMOTION");
        }

        private void HandleQTEPressed()
        {
            _qteCompo.HandleQTEPressed();
        }

        public override void Exit()
        {
            _player.PlayerBattleInput.OnQTEKeyPressed -= HandleQTEPressed;
            _qteCompo.onSuccess.RemoveListener(HandleSuccess);
            _qteCompo.onFailure.RemoveListener(HandleFailure);
            base.Exit();
        }
    }
}