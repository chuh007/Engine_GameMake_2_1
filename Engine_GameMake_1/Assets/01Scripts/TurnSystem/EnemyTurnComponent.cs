using _01Scripts.Enemies;
using _01Scripts.Entities;
using UnityEngine;

namespace _01Scripts.TurnSystem
{
    public class EnemyTurnComponent : EntityTurnComponent
    {
        private Enemy _player;
        
        public override void Initialize(Entity entity)
        {
            base.Initialize(entity);
            _player = entity as Enemy;
        }

        public override void TurnAction()
        {
            base.TurnAction();
            
        }
    }
}