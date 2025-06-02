using _01Scripts.Entities;
using _01Scripts.FSM;

namespace _01Scripts.Players.States.UIInputStates
{
    public class UISelectInputState : EntityState
    {
        protected Player _player;
        
        public UISelectInputState(Entity entity, int animationHash) : base(entity, animationHash)
        {
            _player = entity as Player;
        }

        public override void Enter()
        {
            base.Enter();
            _player.PlayerBattleInput.OnAttackPressed += HandleAttackPressed;
            // _player.PlayerBattleInput.
        }

        private void HandleAttackPressed()
        {
            
        }
        
        public override void Exit()
        {
            base.Exit();
        }

    }
}