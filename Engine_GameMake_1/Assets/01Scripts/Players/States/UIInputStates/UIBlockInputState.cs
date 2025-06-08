using _01Scripts.Core;
using _01Scripts.Entities;
using DG.Tweening;
using UnityEngine;

namespace _01Scripts.Players.States.UIInputStates
{
    public class UIBlockInputState : UIInputState
    {
        private PlayerCostCompo _costCompo;

        private bool _canBlock = false;
        
        public UIBlockInputState(Entity entity, int animationHash) : base(entity, animationHash)
        {
            _costCompo = _player.GetCompo<PlayerCostCompo>();
        }

        public override void Enter()
        {
            base.Enter();
            _canBlock = true;
            PlayerUIInoutComponent.InputUIChanged(ControlUIType.UIBlockInput);
            _player.OnDefense.AddListener(HandleDefense);
            _player.PlayerBattleInput.OnBlockKeyPressed += HandleBlockPressed;

        }

        private void HandleDefense()
        {
            _costCompo.PlusCost(1);
        }

        private void HandleBlockPressed()
        {
            _entity.IsDefense = true;
            DOVirtual.DelayedCall(0.5f, () => _canBlock = true);
            DOVirtual.DelayedCall(1f, () =>
            {
                
                _entity.IsDefense = false;
            });
        }

        public override void Exit()
        {
            _costCompo.PlusCost(1);
            _player.OnDefense.RemoveListener(HandleDefense);
            _player.PlayerBattleInput.OnBlockKeyPressed -= HandleBlockPressed;
            base.Exit();
        }
    }
}