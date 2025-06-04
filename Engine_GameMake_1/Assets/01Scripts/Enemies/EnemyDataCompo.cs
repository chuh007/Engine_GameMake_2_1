using System;
using _01Scripts.Entities;
using Code.Core.GameSystem;
using UnityEngine;

namespace _01Scripts.Enemies
{
    public class EnemyDataCompo : MonoBehaviour, IEntityComponent, ISavable
    {
        [SerializeField] private EnemyDataSO enemyData;
        
        private Enemy _enemy;
        
        public void Initialize(Entity entity)
        {
            _enemy = entity as Enemy;
        }

        [field: SerializeField] public SaveIdSO SaveID { get; private set; }
        
        [Serializable]
        public struct EnemySaveData
        {
            public EnemyDataSO enemyData;
        }
        
        public string GetSaveData()
        {
            EnemySaveData data = new EnemySaveData
            {
                enemyData = enemyData
            };
            return JsonUtility.ToJson(data);
        }

        public void RestoreData(string loadedData)
        {
            EnemySaveData loadData = JsonUtility.FromJson<EnemySaveData>(loadedData);
            
            enemyData = loadData.enemyData;
        }
    }
}