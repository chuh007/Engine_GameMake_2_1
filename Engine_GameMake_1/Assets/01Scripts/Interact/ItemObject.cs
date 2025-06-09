using _01Scripts.Core.EventSystem;
using _01Scripts.Items;
using UnityEngine;

namespace _01Scripts.Interact
{
    public class ItemObject : MonoBehaviour, IInteractable
    {
        [field:SerializeField] public string Name { get; set; }
        [SerializeField] private ItemDataSO itemData;
        [SerializeField] private GameEventChannelSO inventoryChannel;

        
        public void Interact()
        {
            inventoryChannel.RaiseEvent(InventoryEvents.AddItemEvent.Initializer(itemData));
            Destroy(gameObject);
        }
    }
}