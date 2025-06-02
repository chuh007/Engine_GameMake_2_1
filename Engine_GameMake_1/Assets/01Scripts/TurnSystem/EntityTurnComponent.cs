using _01Scripts.Entities;
using Chuh007Lib.StatSystem;
using UnityEngine;

namespace _01Scripts.TurnSystem
{
    public class EntityTurnComponent : MonoBehaviour, IEntityComponent, ITurnActor
    {
        [SerializeField] private StatSO speedStat;
        
        
        public void Initialize(Entity entity)
        {
            Speed = (int)entity.GetCompo<EntityStat>().GetStat(speedStat).Value;
        }

        public int Speed { get; set; }
        public int ActionValue { get; set; } = 0;
        public void TurnAction()
        {
            Debug.Log(transform.parent.name + "의 턴");
        }
    }
}