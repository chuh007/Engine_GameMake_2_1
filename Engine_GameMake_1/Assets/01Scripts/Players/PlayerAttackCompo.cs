using _01Scripts.Combat;
using _01Scripts.Entities;
using UnityEngine;

namespace _01Scripts.Players
{
    public struct DamageData
    {
        public float damage;
    }
    
    public class PlayerAttackCompo : EntityAttackCompo
    {
        public void SetTarget(Entity target)
        {
            _target = target;
        }
        
        public override void Attack()
        {
            base.Attack();
            DamageData data = new DamageData();
            data.damage = _damage;
            _target.ApplyDamage(data, _entity);
        }

        public override void EndAttack()
        {
            base.EndAttack();
        }
    }
}