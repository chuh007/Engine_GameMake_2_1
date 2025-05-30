using _01Scripts.Entities;
using UnityEngine;

namespace _01Scripts.Players.States
{
    public class PlayerSearchIdleState : PlayerSearchState
    {

        public PlayerSearchIdleState(Entity entity, int animationHash) : base(entity, animationHash)
        {
        }

        public override void Update()
        {
            base.Update();
            Vector2 movementKey = _player.PlayerInput.MovementKey;
            
            _movement.SetMovementDirection(movementKey);
            if (movementKey.magnitude > _inputThreshold)
                _player.ChangeState("MOVE");
        }

    }
}

