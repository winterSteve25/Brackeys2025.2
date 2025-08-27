using System.Collections.Generic;
using InventorySystem;
using Items;
using KBCore.Refs;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Player
{
    public class PlayerItemUsage : ValidatedMonoBehaviour
    {
        [Header("Debug Info DO NOT EDIT")]
        private UpdatableItem _itemSelected;
        
        [Header("References")]
        [SerializeField, Self] private PlayerInventory inventory;
        [SerializeField] private Transform handAnchor;

        [Header("Parameters")]
        [SerializeField] private bool usableItems;

        private void Start()
        {
            inventory.OnSelectedItemChanged += InventoryOnOnSelectedItemChanged;
        }

        private void OnDestroy()
        {
            inventory.OnSelectedItemChanged -= InventoryOnOnSelectedItemChanged;
        }
        
        private void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
            if (!usableItems) return;
            if (_itemSelected == null) return;
            _itemSelected.UseTick(inventory);
        }
        
        private void InventoryOnOnSelectedItemChanged(ItemStack obj)
        {
            if (obj == null || obj.itemType.Prefab == null)
            {
                if (_itemSelected == null) return;
                Destroy(_itemSelected.gameObject);
                return;
            }
            
            if (_itemSelected != null)
            {
                Destroy(_itemSelected.gameObject);
            }

            var item = Instantiate(obj.itemType.Prefab, handAnchor);
            _itemSelected = item;
            _itemSelected.item = obj;
        }
    }
}