using _01Scripts.Entities;
using UnityEngine;

namespace _01Scripts.Combat
{

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
            Debug.Log("플레이어의 공격");
            _target.ApplyDamage(data, _entity);
        }

        public override void EndAttack()
        {
            base.EndAttack();
        }
    }
}