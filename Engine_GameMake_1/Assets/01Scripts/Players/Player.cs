using _01Scripts.Entities;
using _01Scripts.FSM;
using _01Scripts.TurnSystem;
using Chuh007Lib.Dependencies;
using UnityEngine;

namespace _01Scripts.Players
{
    public enum PlayerType
    {
        Search, Battle
    }
    
    public class Player : Entity, IDependencyProvider
    {
        [SerializeField] private PlayerType playerType;
        [field: SerializeField] public PlayerInputSO PlayerInput { get; private set; }
        [field: SerializeField] public PlayerBattleInputSO PlayerBattleInput { get; private set; }

        [SerializeField] private StateDataSO[] states;
        
        private EntityStateMachine _stateMachine;

        [Provide]
        public Player ProvidePlayer() => this;
        
        protected override void Awake()
        {
            base.Awake();
            _stateMachine = new EntityStateMachine(this, states);
            if (playerType == PlayerType.Search)
            {
                PlayerInput.SetCallbacks();
                PlayerBattleInput.RemoveCallbacks();
            }
            else if (playerType == PlayerType.Battle)
            {
                PlayerInput.RemoveCallbacks();
                PlayerBattleInput.SetCallbacks();
            }
        }

        protected override void HandleHit()
        {
            
        }

        protected override void HandleDead(Entity entity)
        {
            
        }

        private void OnDisable()
        {
        }

        private void Start()
        {
            if(playerType == PlayerType.Search) _stateMachine.ChangeState("IDLE");
            if(playerType == PlayerType.Battle) _stateMachine.ChangeState("UIBLOCK");
        }

        private void Update()
        {
            _stateMachine.UpdateStateMachine();
        }

        public void ChangeState(string newStatName) => _stateMachine.ChangeState(newStatName);

    }
}