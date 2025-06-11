using _01Scripts.Combat;
using _01Scripts.Core;
using _01Scripts.Core.EventSystem;
using _01Scripts.Entities;
using UnityEngine;

namespace _01Scripts.Players.States.UIInputStates
{
    public class UIUseItemInputState : UIInputState
    {
        private PlayerInvenData _playerInvenData;
        private EntityHealthComponent _healthComponent;
        
        public UIUseItemInputState(Entity entity, int animationHash) : base(entity, animationHash)
        {
            _playerInvenData = entity.GetCompo<PlayerInvenData>();
            _healthComponent = entity.GetCompo<EntityHealthComponent>();
        }

        public override void Enter()
        {
            base.Enter();
            _player.PlayerBattleInput.OnCancelOrESCKeyPressed += HandleCancelOrEscKeyPressed;
            _player.PlayerBattleInput.OnSelect1KeyPressed += HandleSelect1KeyPressed;
            _player.PlayerBattleInput.OnSelect2KeyPressed += HandleSelect2KeyPressed;
            PlayerUIInoutComponent.InputUIChanged(ControlUIType.UIUseItemSelect);
            InventoryDataEvent evt = InventoryEvents.InventoryDataEvent;
            evt.slotCount = 2;
            evt.items = _playerInvenData.inventory;
            _playerInvenData.inventoryChannel.RaiseEvent(evt);
        }

        private void HandleCancelOrEscKeyPressed()
        {
            _player.ChangeState("UISELECT");
        }

        private void HandleSelect1KeyPressed()
        {
            if (_playerInvenData.CanRemoveItem(_playerInvenData.healItemData))
            {
                _playerInvenData.RemoveItem(_playerInvenData.healItemData);
                _healthComponent.ApplyHeal(20);
                TurnEndEvent evt = TurnEvents.TurnEndEvent;
                _player.TurnChannel.RaiseEvent(evt);
                _player.ChangeState("UIBLOCK");
            }
        }

        private void HandleSelect2KeyPressed()
        {
            if (_playerInvenData.CanRemoveItem(_playerInvenData.healItemData))
            {
                
                _playerInvenData.RemoveItem(_playerInvenData.healItemData);
            }
        }
        
        public override void Exit()
        {
            _player.PlayerBattleInput.OnCancelOrESCKeyPressed -= HandleCancelOrEscKeyPressed;
            _player.PlayerBattleInput.OnSelect1KeyPressed -= HandleSelect1KeyPressed;
            _player.PlayerBattleInput.OnSelect2KeyPressed -= HandleSelect2KeyPressed;
            base.Exit();
        }
    }
}