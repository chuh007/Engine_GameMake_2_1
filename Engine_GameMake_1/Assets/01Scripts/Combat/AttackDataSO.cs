using _01Scripts.Effects;
using UnityEngine;

namespace _01Scripts.Combat
{
    [CreateAssetMenu(fileName = "AttackData", menuName = "SO/Combat/AttackData", order = 0)]
    public class AttackDataSO : ScriptableObject
    {
        public string attackName;
        public float damageMultiplier = 1f;
        public float damageIncrease = 0;

        public AnimationClip attackAnimation;
        public PlayParticleVFX particle;
        public int triggerCount;
        
        private void OnEnable()
        {
            attackName = this.name;
        }
    }
}