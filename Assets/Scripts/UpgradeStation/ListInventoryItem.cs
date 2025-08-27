using InventorySystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UpgradeStation
{
    public class ListInventoryItem : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text itemName;
        [SerializeField] private TMP_Text itemCost;

        public void Initialize(ItemStack item)
        {
            icon.sprite = item.itemType.Icon;
            itemName.text = $"{item.itemType.Name} x{item.amount}";
            // itemCost.text 
        }
    }
}