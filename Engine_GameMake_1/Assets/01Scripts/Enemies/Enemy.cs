using System;
using _01Scripts.Core.EventSystem;
using _01Scripts.Entities;
using _01Scripts.FSM;
using _01Scripts.Players;
using _01Scripts.TurnSystem;
using Chuh007Lib.Dependencies;
using UnityEngine;

namespace _01Scripts.Enemies
{
    public class Enemy : Entity
    {
        [Inject, HideInInspector] public Player target;

        [SerializeField] private GameEventChannelSO spawnChannel;
        [SerializeField] private GameEventChannelSO enemyChannel;

        [SerializeField] private StateDataSO[] states;
        
        private EntityStateMachine _stateMachine;
        
        protected override void Awake()
        {
            base.Awake();
            SpawnEventRaise();
            _stateMachine = new EntityStateMachine(this, states);
        }

        public void SpawnEventRaise()
        {
            var evt = SpawnEvents.SpawnEntityEvent;
            evt.Entity = this;
            spawnChannel.RaiseEvent(evt);
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