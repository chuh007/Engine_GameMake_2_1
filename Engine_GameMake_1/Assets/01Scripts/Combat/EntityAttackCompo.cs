using System.Collections.Generic;
using _01Scripts.Entities;
using Chuh007Lib.StatSystem;
using UnityEngine;

namespace _01Scripts.Combat
{
    public struct DamageData
    {
        public float damage;
    }

    public abstract class EntityAttackCompo : MonoBehaviour, IEntityComponent
    {
        [SerializeField] protected StatSO damageStat;
        [SerializeField] protected List<AttackDataSO> attackDataList;
        
        protected Entity _entity, _target;
        protected EntityAnimator _entityAnimator;
        protected EntityStat _entityStat;
        protected AttackDataSO _currentAttackData;

        protected float _damage;
        
        public virtual void Initialize(Entity entity)
        {
            _entity = entity;
            _entityAnimator = _entity.GetCompo<EntityAnimator>();
            _entityStat = _entity.GetCompo<EntityStat>();
            _damage = _entityStat.GetStat(damageStat).Value;
        }
        
        public virtual void Attack()
        {
        }

        public virtual void EndAttack()
        {
        }
    }
}