using Blade.Entities;
using Blade.FSM;
using Blade.Players;

namespace _01Scripts.Players.States
{
    public abstract class PlayerState : EntityState
    {
        protected Player _player;
        protected readonly float _inputThreshold = 0.1f;

        protected CharacterMovement _movement;
        public PlayerState(Entity entity, int animationHash) : base(entity, animationHash)
        {
            _player = entity as Player;
            _movement = entity.GetCompo<CharacterMovement>();
        }
    }

}
