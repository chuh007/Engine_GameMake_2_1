using _01Scripts.Core.EventSystem;
using _01Scripts.Entities;
using _01Scripts.TurnSystem;
using UnityEngine;

namespace _01Scripts.Enemies
{
    public class Enemy : Entity
    {
        [SerializeField] private GameEventChannelSO spawnChannel;

        protected override void Awake()
        {
            base.Awake();
            SpawnEventRaise();
        }

        public void SpawnEventRaise()
        {
            var evt = SpawnEvents.SpawnEntityEvent;
            evt.Entity = this;
            spawnChannel.RaiseEvent(evt);
        }
    }
}