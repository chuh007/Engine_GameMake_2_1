using Blade.Entities;
using UnityEngine;

namespace _01Scripts.Players.States
{
    public class PlayerSearchMoveState : PlayerState
    {
        
        public PlayerSearchMoveState(Entity entity, int animationHash) : base(entity, animationHash)
        {
        }

        public override void Update()
        {
            base.Update();
            Vector2 movementKey = _player.PlayerInput.MovementKey;
            
            _movement.SetMovementDirection(movementKey);
            if (movementKey.magnitude < _inputThreshold)
                _player.ChangeState("IDLE");
        }

    }
}

