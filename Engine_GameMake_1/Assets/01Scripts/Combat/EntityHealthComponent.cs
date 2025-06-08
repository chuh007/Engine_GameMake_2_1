using _01Scripts.Entities;
using _01Scripts.Players;
using Chuh007Lib.StatSystem;
using UnityEngine;
using UnityEngine.Events;

namespace _01Scripts.Combat
{
    public class EntityHealthComponent : MonoBehaviour, IEntityComponent, IAfterInitialize
    {
        public UnityEvent<float> currentHpValueChangeEvent;
        
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
            currentHpValueChangeEvent?.Invoke(_currentHealth);
            Debug.Log(_currentHealth);
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
            Debug.Log(_currentHealth + "로바끼ㅣㅁ");
            _currentHealth = Mathf.Clamp(_currentHealth + current - previous, 1f, maxHealth);
            currentHpValueChangeEvent?.Invoke(_currentHealth);
        }
        
        private void ApplyDamage(DamageData damageData, Entity dealer)
        {
            if (_entity.IsDead) return;
            Debug.Log(_currentHealth);
            _currentHealth = Mathf.Clamp(_currentHealth - damageData.damage, 0, maxHealth);
            // Debug.Log(_currentHealth);
            // Debug.Log(damageData.damage);
            // Debug.Log(maxHealth);
            Debug.Log($"{damageData.damage} 받음. 남은 HP : {_currentHealth}");
            currentHpValueChangeEvent?.Invoke(_currentHealth);
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
            Debug.Log(_currentHealth);
        }
    }
}