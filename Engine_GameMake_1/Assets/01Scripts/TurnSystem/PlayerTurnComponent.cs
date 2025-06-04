using _01Scripts.Entities;
using _01Scripts.Players;
using UnityEngine;

namespace _01Scripts.TurnSystem
{
    public class PlayerTurnComponent : EntityTurnComponent
    {
        private Player _player;
        
        public override void Initialize(Entity entity)
        {
            base.Initialize(entity);
            _player = entity as Player;
        }

        public override void TurnAction()
        {
            base.TurnAction();
            _player.ChangeState("UISELECT");
        }
    }
}