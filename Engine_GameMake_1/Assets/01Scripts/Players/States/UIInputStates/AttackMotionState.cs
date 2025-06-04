using _01Scripts.Core;
using _01Scripts.Core.EventSystem;
using _01Scripts.Entities;
using DG.Tweening;
using UnityEngine;

namespace _01Scripts.Players.States.UIInputStates
{
    public class AttackMotionState : UIInputState
    {
        private PlayerAttackCompo _attackCompo;
        private PlayerTargetSelector _targetSelector;
        private EntityAnimator _animator;
        private EntityAnimatorTrigger _animTrigger;
        
        private readonly int _attackAnimationHash = Animator.StringToHash("ATTACK");
        private Vector3 orginPos;
        
        public AttackMotionState(Entity entity, int animationHash) : base(entity, animationHash)
        {
            _attackCompo = entity.GetCompo<PlayerAttackCompo>();
            _targetSelector = entity.GetCompo<PlayerTargetSelector>();
            _animator = entity.GetCompo<EntityAnimator>();
            _animTrigger = entity.GetCompo<EntityAnimatorTrigger>();
        }

        public override void Enter()
        {
            base.Enter();
            orginPos = _player.transform.position;
            PlayerUIInoutComponent.InputUIChanged(ControlUIType.UIBlockInput);
            _animTrigger.OnAttackTrigger += HandleAttackTrigger;
            Vector3 targetPos = _targetSelector.CurrentTarget.transform.position;
            _player.transform.DOMove(targetPos - (targetPos - _player.transform.position).normalized, 0.25f).OnComplete(() =>
            {
                _animator.SetParam(_attackAnimationHash);
            });
        }

        private void HandleAttackTrigger()
        {
            _attackCompo.Attack();
            _player.ChangeState("UIBLOCK");
            _player.transform.DOMove(orginPos, 0.25f).OnComplete(() =>
            {
                TurnEndEvent evt = TurnEvents.TurnEndEvent;
                _player.TurnChannel.RaiseEvent(evt);
            });

        }

        public override void Exit()
        {
            _animTrigger.OnAttackTrigger -= HandleAttackTrigger;
            base.Exit();
        }
    }
}