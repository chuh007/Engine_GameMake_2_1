using UnityEngine;

namespace _01Scripts.Interact
{
    public interface IInteractable
    {
        string Name { get; }

        public void Interact();
    }
}