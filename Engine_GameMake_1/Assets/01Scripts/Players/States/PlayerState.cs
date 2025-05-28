using _01Scripts.Entities;
using _01Scripts.FSM;
using UnityEngine;

namespace _01Scripts.Players.States
{
    public abstract class PlayerState : EntityState
    {
        protected Player _player;
        protected readonly float _inputThreshold = 0.1f;

        protected CharacterMovement _movement;
        protected PlayerCamRotator _camRotator;
        
        public PlayerState(Entity entity, int animationHash) : base(entity, animationHash)
        {
            _player = entity as Player;
            _movement = entity.GetCompo<CharacterMovement>();
            _camRotator = entity.GetCompo<PlayerCamRotator>();
        }

        public override void Update()
        {
            base.Update();
            Vector2 rotateKey = _player.PlayerInput.LookKey;
            _camRotator.SetMouseDirection(rotateKey);
        }
    }

}
