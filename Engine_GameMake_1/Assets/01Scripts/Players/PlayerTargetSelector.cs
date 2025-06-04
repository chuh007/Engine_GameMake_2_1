using System;
using System.Collections.Generic;
using _01Scripts.Core.EventSystem;
using _01Scripts.Entities;
using UnityEngine;

namespace _01Scripts.Players
{
    public class PlayerTargetSelector : MonoBehaviour, IEntityComponent
    {
        public Entity CurrentTarget { get; private set; }
        
        [SerializeField] private GameEventChannelSO spawnChannel;

        private List<Entity> _targets = new List<Entity>();
        private Player _player;
        
        public void Initialize(Entity entity)
        {
            _player = entity as Player;
        }

        private void Awake()
        {
            spawnChannel.AddListener<SpawnEntityEvent>(HandleSpawnEntity);
        }

        private void HandleSpawnEntity(SpawnEntityEvent obj)
        {
            _targets.Add(obj.Entity);
            CurrentTarget = _targets[0];
        }

        private void OnDestroy()
        {
            spawnChannel.RemoveListener<SpawnEntityEvent>(HandleSpawnEntity);
        }
    }
}