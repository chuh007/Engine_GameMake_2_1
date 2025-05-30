using System;
using _01Scripts.Entities;
using _01Scripts.Interact;
using UnityEngine;

namespace _01Scripts.Players
{
    public class PlayerInteract : MonoBehaviour, IEntityComponent
    {
        [SerializeField] private Transform cameraTrm;
        [SerializeField] private LayerMask whatIsTarget;
        
        private Player _player;
        private IInteractable _target;
        
        public void Initialize(Entity entity)
        {
            _player = entity as Player;
        }

        private void Update()
        {
            Physics.Raycast(cameraTrm.position, cameraTrm.forward, out RaycastHit hit, 3f, whatIsTarget);
            if (hit.transform == null)
            {
                _target = null;
                return;
            }
            if (hit.transform.TryGetComponent(out IInteractable interactable))
            {
                _target = interactable;
            }
        }

        public void Interact()
        {
            _target?.Interact();
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(cameraTrm.position, cameraTrm.forward * 5);
        }
    }
}