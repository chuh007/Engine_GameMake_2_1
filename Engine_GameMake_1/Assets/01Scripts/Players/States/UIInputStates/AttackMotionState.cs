using _01Scripts.Combat;
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
        private PlayerCamShaker _shaker;
        private EntityAnimator _animator;
        private EntityAnimatorTrigger _animTrigger;
        
        private Vector3 _orginPos;
        
        public AttackMotionState(Entity entity, int animationHash) : base(entity, animationHash)
        {
            _attackCompo = entity.GetCompo<PlayerAttackCompo>();
            _targetSelector = entity.GetCompo<PlayerTargetSelector>();
            _animator = entity.GetCompo<EntityAnimator>();
            _animTrigger = entity.GetCompo<EntityAnimatorTrigger>();
            _shaker = entity.GetCompo<PlayerCamShaker>();
        }

        public override void Enter()
        {
            base.Enter();
            _orginPos = _player.transform.position;
            PlayerUIInoutComponent.InputUIChanged(ControlUIType.UIBlockInput);
            _animTrigger.OnAttackTrigger += HandleAttackTrigger;
            _animTrigger.OnAnimationEndTrigger += HandleAnimationEndTrigger;
            Vector3 targetPos = _targetSelector.CurrentTarget.transform.position;
            _player.transform.DOMove(targetPos - (targetPos - _player.transform.position).normalized, 0.25f).OnComplete(() =>
            {
                _animator.SetParam(Animator.StringToHash(_attackCompo.currentAttackData.attackAnimationName));
            });
        }

        private void HandleAttackTrigger()
        {
            _shaker.ShakeCam();
            _attackCompo.Attack();
        }

        private void HandleAnimationEndTrigger()
        {
            _player.ChangeState("UIBLOCK");
            _player.transform.DOMove(_orginPos, 0.25f).OnComplete(() =>
            {
                TurnEndEvent evt = TurnEvents.TurnEndEvent;
                _player.TurnChannel.RaiseEvent(evt);
            });

        }

        public override void Exit()
        {
            _animTrigger.OnAnimationEndTrigger -= HandleAnimationEndTrigger;
            _animTrigger.OnAttackTrigger -= HandleAttackTrigger;
            base.Exit();
        }
    }
}