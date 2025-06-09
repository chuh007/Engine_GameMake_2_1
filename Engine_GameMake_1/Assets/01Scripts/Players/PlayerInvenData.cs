using System;
using System.Collections.Generic;
using System.Linq;
using _01Scripts.Core.EventSystem;
using _01Scripts.Entities;
using _01Scripts.Items;
using _01Scripts.Items.Inven;
using Code.Core.GameSystem;
using UnityEditor;
using UnityEngine;

namespace _01Scripts.Players
{
    public class PlayerInvenData : InvenData, IEntityComponent, IAfterInitialize, ISavable
    {
        [SerializeField] private GameEventChannelSO inventoryChannel;
        
        private Player _player;
        private EntityStat _statCompo;

        public void Initialize(Entity entity)
        {
            _player = entity as Player;
            inventory = new List<InventoryItem>(); //인벤토리에 빈칸 만들어주고
            
            _statCompo = entity.GetCompo<EntityStat>();
        }

        public void AfterInitialize()
        {
            inventoryChannel.AddListener<RequestInventoryDataEvent>(HandleRequestPlayerInvenData);
            inventoryChannel.AddListener<AddItemEvent>(HandleAddItem);
        }
        
        private void OnDestroy()
        {
            inventoryChannel.RemoveListener<RequestInventoryDataEvent>(HandleRequestPlayerInvenData);
            inventoryChannel.RemoveListener<AddItemEvent>(HandleAddItem);
        }
        
        private void HandleRequestPlayerInvenData(RequestInventoryDataEvent evt)
        {
            UpdateInventoryUI();
        }

        private void UpdateInventoryUI()
        {
            inventoryChannel.RaiseEvent(
                InventoryEvents.InventoryDataEvent.Initializer(99, inventory));
            if (inventory.Count > 0)
            {
                Debug.Log(inventory[0].data);
            }
        }
        private void HandleAddItem(AddItemEvent evt) => AddItem(evt.itemData);
        
        public override void AddItem(ItemDataSO itemData, int count = 1)
        {
            IEnumerable<InventoryItem> items = GetItems(itemData);
            InventoryItem canAddItem = items.FirstOrDefault(item => item.IsFullStack == false);

            if (canAddItem == default)
            {
                Debug.Log("새로운 칸에 넣다");
                CreateNewInventory(itemData, count); //풀스택이 false인 칸이 없다면 새로 칸을 만들어서 넣는다.
            }
            else
            {
                Debug.Log("기존 칸에 넣다");
                int remain = canAddItem.AddStack(count); //주운만큼 넣어지고 남는건 반환
                if(remain > 0)
                    CreateNewInventory(itemData, remain);
            }
            
            UpdateInventoryUI();
        }
        
        private void CreateNewInventory(ItemDataSO itemData, int count)
        {
            InventoryItem newItem = new InventoryItem(itemData, count);
            inventory.Add(newItem);
        }

        public override void RemoveItem(ItemDataSO itemData, int count)
        {
            UpdateInventoryUI(); //제거는 나중에 만들고 나서
        }
        
        public override bool CanAddItem(ItemDataSO itemData)
        {
            if (inventory.Count < 99 - 1) return true;
            
            IEnumerable<InventoryItem> items = GetItems(itemData);
            InventoryItem canAddItem = items.FirstOrDefault(item => item.IsFullStack == false);
            
            return canAddItem != default; 
        }
        
#if UNITY_EDITOR
        [ContextMenu("Load all item data")]
        private void LoadAllItemData()
        {
            if (itemDB == null)
            {
                Debug.LogWarning("No item DB found");
                return;
            }

            string path = "Assets/08SO/ItemData";
            string[] assetNames = AssetDatabase.FindAssets("", new[] {path});
            itemDB.ClearAllItems();
            foreach (string guid in assetNames)
            {
                string soPath = AssetDatabase.GUIDToAssetPath(guid);
                ItemDataSO itemData = AssetDatabase.LoadAssetAtPath<ItemDataSO>(soPath);
                if (itemData != null)
                {
                    if(itemData is ItemDataSO scrapItemDataSO)
                        itemDB.scrapItems.Add(scrapItemDataSO);
                }
            }
            
            EditorUtility.SetDirty(itemDB);
            AssetDatabase.SaveAssets();
        }
        

#endif
        
        #region SaveLogic

        [SerializeField] private ItemDatabaseSO itemDB;
        [field: SerializeField] public SaveIdSO SaveID { get; private set; }

        [Serializable]
        public struct InvenItemSaveData
        {
            public string itemId;
            public int stackSize;
            public int slotIndex;
        }

        
        [Serializable]
        public struct InvenSaveData
        {
            public List<InvenItemSaveData> items;
        }
        
        public string GetSaveData()
        {
            
            InvenSaveData saveData;
            saveData.items = inventory.Select((item, idx) => new InvenItemSaveData
            {
                itemId = item.data.itemID,
                stackSize = item.stackSize,
                slotIndex = idx
            }).ToList();

            
            return JsonUtility.ToJson(saveData);
        }
        
        public void RestoreData(string loadedData)
        {
            InvenSaveData loadedSaveData = JsonUtility.FromJson<InvenSaveData>(loadedData);
            loadedSaveData.items.Sort((item1, item2) => item1.slotIndex - item2.slotIndex); //오름차순으로 정렬
            inventory = loadedSaveData.items.Select(saveItem =>
            {
                ItemDataSO itemData = itemDB.GetItem(saveItem.itemId); //아이디를 기반으로 아이템 데이터 불러온다.
                Debug.Assert(itemData != null, $"Save data corrupted : {saveItem.itemId} is not exist on DB");
                
                return new InventoryItem(itemData, saveItem.stackSize);
            }).ToList();
        }

        #endregion
    }
}