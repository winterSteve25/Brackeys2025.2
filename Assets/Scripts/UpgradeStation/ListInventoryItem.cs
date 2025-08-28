using InventorySystem;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utils;

namespace UpgradeStation
{
    public class ListInventoryItem : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text itemName;
        [SerializeField] private TMP_Text itemCost;
        [SerializeField] private Color sellColor;
        [SerializeField] private TMP_Text sellOrBuyTxt;

        private TetrisInventory _inventory;
        private ItemStack _itemStack;

        public void Initialize(TetrisInventory inventory, ItemStack item, TMP_Text sellOrBuyAmount)
        {
            icon.sprite = item.itemType.Icon;
            itemName.text = $"{item.itemType.Name} x{item.amount}";
            itemCost.text = item.Price.ToString();
            sellOrBuyTxt = sellOrBuyAmount;
            _inventory = inventory;
            _itemStack = item;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            CarryOverDataManager.Instance.Gold += _itemStack.Price;
            _inventory.RemoveItem(_itemStack.position);
            Destroy(gameObject);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            sellOrBuyTxt.color = sellColor;
            sellOrBuyTxt.text = $"+{_itemStack.Price}";
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)sellOrBuyTxt.transform.parent);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            sellOrBuyTxt.text = "";
        }
    }
}