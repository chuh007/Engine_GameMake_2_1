using System.Collections.Generic;
using _01Scripts.Combat;
using UnityEngine;

namespace _01Scripts.Enemies
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "SO/Enemy/Data", order = 0)]
    public class EnemyDataSO : ScriptableObject
    {
        public float health;
        public float damage;
        public float speed;
        public string visualName;
        
        public List<AttackDataSO> attackDataList;
        
    }
}