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
        [SerializeField] private Color buyColor;
        [SerializeField] private TMP_Text sellOrBuyTxt;
        [SerializeField] private bool buy;
        
        [SerializeField] private Image background;
        [SerializeField] private Sprite nonHoverSprite;
        [SerializeField] private Sprite hoverSprite;

        private TetrisInventory _inventory;
        private TetrisInventory _buyInto;
        private ItemStack _itemStack;

        public void Initialize(TetrisInventory inventory, TetrisInventory buyInto, ItemStack item, TMP_Text sellOrBuyAmount)
        {
            icon.sprite = item.itemType.Icon;
            icon.GetComponent<AspectRatioFitter>().aspectRatio = item.itemType.Size.x / (float) item.itemType.Size.y;
            
            itemName.text = $"{item.itemType.Name} x{item.amount}";
            itemCost.text = item.Price.ToString();
            sellOrBuyTxt = sellOrBuyAmount;
            _inventory = inventory;
            _buyInto = buyInto;
            _itemStack = item;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _inventory.RemoveItem(_itemStack.position);
            
            if (buy)
            {
                CarryOverDataManager.Instance.Gold -= _itemStack.Price;
                _buyInto.AddAnywhere(_itemStack);
            }
            else
            {
                CarryOverDataManager.Instance.Gold += _itemStack.Price;
            }

            Destroy(gameObject);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (buy)
            {
                sellOrBuyTxt.color = buyColor;
                sellOrBuyTxt.text = $"-{_itemStack.Price}";
            }
            else
            {
                sellOrBuyTxt.color = sellColor;
                sellOrBuyTxt.text = $"+{_itemStack.Price}";
            }

            background.sprite = hoverSprite;
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)sellOrBuyTxt.transform.parent);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            sellOrBuyTxt.text = "";
            background.sprite = nonHoverSprite;
        }
    }
}