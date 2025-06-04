using _01Scripts.Entities;
using _01Scripts.Players;
using Chuh007Lib.StatSystem;
using UnityEngine;

namespace _01Scripts.Combat
{
    public class EntityHealthComponent : MonoBehaviour, IEntityComponent, IAfterInitialize
    {
        [SerializeField] private StatSO hpStat;
        public float maxHealth;
        private float _currentHealth;
        
        private Entity _entity;
        private EntityStat _statCompo;
        private EntityFeedbackData _feedbackData;
        
        public void Initialize(Entity entity)
        {
            _entity = entity;
            _statCompo = _entity.GetCompo<EntityStat>();
            _feedbackData = _entity.GetCompo<EntityFeedbackData>();
        }
        
        public void AfterInitialize()
        {
            _statCompo.GetStat(hpStat).OnValueChanged += HandleHPChange;
            _currentHealth = maxHealth = _statCompo.GetStat(hpStat).Value;
            _entity.OnDamage += ApplyDamage;
        }

        private void OnDestroy()
        {
            _statCompo.GetStat(hpStat).OnValueChanged -= HandleHPChange;
            _entity.OnDamage -= ApplyDamage;
        }
        
        private void HandleHPChange(StatSO stat, float current, float previous)
        {
            maxHealth = current;
            _currentHealth = Mathf.Clamp(_currentHealth + current - previous, 1f, maxHealth);
        }
        
        private void ApplyDamage(DamageData damageData, Entity dealer)
        {
            if (_entity.IsDead) return;
            Debug.Log($"{damageData.damage} 받음. 남은 HP : {_currentHealth}");
            _currentHealth = Mathf.Clamp(_currentHealth - damageData.damage, 0, maxHealth);
            _feedbackData.LastEntityWhoHit = dealer;
            AfterHitFeedbacks();
        }

        private void AfterHitFeedbacks()
        {
            _entity.OnHit?.Invoke();
            if (_currentHealth <= 0)
            {
                _entity.OnDead?.Invoke(_entity);
            }
        }

        public void RestoreHealth(float restoreHealth)
        {
            _currentHealth = restoreHealth;
        }
    }
}