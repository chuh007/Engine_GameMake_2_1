using _01Scripts.Entities;
using UnityEngine;

namespace _01Scripts.Combat
{
    public abstract class EntityAttackCompo : MonoBehaviour, IEntityComponent
    {
        private Entity _entity;
        private EntityAnimator _entityAnimator;
        
        public virtual void Initialize(Entity entity)
        {
            _entity = entity;
            _entityAnimator = _entity.GetCompo<EntityAnimator>();
        }
        
        public virtual void Attack()
        {
        }

        public virtual void EndAttack()
        {
        }
    }
}