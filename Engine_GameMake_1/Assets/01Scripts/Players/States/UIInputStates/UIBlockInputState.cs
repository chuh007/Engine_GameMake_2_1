using _01Scripts.Core;
using _01Scripts.Entities;
using UnityEngine;

namespace _01Scripts.Players.States.UIInputStates
{
    public class UIBlockInputState : UIInputState
    {
        public UIBlockInputState(Entity entity, int animationHash) : base(entity, animationHash)
        {
        }

        public override void Enter()
        {
            base.Enter();
            PlayerUIInoutComponent.InputUIChanged(ControlUIType.UIBlockInput);
        }
    }
}