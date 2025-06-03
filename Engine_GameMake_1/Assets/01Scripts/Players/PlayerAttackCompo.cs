using _01Scripts.Combat;
using _01Scripts.Entities;
using UnityEngine;

namespace _01Scripts.Players
{
    public class PlayerAttackCompo : EntityAttackCompo
    {
        private Entity _entity;
        private EntityAnimator _entityAnimator;

        
        
        public override void Attack()
        {
            base.Attack();
        }

        public override void EndAttack()
        {
            base.EndAttack();
        }
    }
}