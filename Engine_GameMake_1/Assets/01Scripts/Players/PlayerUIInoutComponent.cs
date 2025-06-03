using _01Scripts.Core;
using _01Scripts.Core.EventSystem;
using _01Scripts.Entities;
using UnityEngine;

namespace _01Scripts.Players
{
    public class PlayerUIInoutComponent : MonoBehaviour, IEntityComponent
    {
        [SerializeField] private GameEventChannelSO cameraChannel;
        [SerializeField] private GameEventChannelSO playerChannel;

        private Player _player;
        
        public void Initialize(Entity entity)
        {
            _player = entity as Player;
        }

        public void InputUIChanged(ControlUIType cameraType)
        {
            var camEvt = CameraEvents.CameraChangeEvent;
            camEvt.CameraType = cameraType;
            cameraChannel.RaiseEvent(camEvt);
            var uiEvt = PlayerEvents.PlayerUIChangeEvent;
            uiEvt.ControlUIType = cameraType;
            playerChannel.RaiseEvent(uiEvt);
        }
    }
}