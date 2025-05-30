using _01Scripts.Entities;
using _01Scripts.FSM;
using Chuh007Lib.Dependencies;
using UnityEngine;

namespace _01Scripts.Players
{
    public class Player : Entity, IDependencyProvider
    {
        [field: SerializeField] public PlayerInputSO PlayerInput { get; private set; }

        [SerializeField] private StateDataSO[] states;

        private EntityStateMachine _stateMachine;

        [Provide]
        public Player ProvidePlayer() => this;
        
        protected override void Awake()
        {
            base.Awake();
            _stateMachine = new EntityStateMachine(this, states);

        }

        private void OnDisable()
        {
        }

        private void Start()
        {
            _stateMachine.ChangeState("IDLE");
        }

        private void Update()
        {
            _stateMachine.UpdateStateMachine();
        }

        public void ChangeState(string newStatName) => _stateMachine.ChangeState(newStatName);

    }
}