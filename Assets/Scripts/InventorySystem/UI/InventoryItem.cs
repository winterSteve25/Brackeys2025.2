using KBCore.Refs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace InventorySystem.UI
{
    public class InventoryItem : MonoBehaviour
    {
        [Header("Debug Info DO NOT EDIT")]
        [field: SerializeField] public ItemStack ItemStackStored { get; private set; }
        [SerializeField] private Transform anchor;

        [Header("References")]
        [SerializeField, Child(Flag.ExcludeSelf)] private Image icon;
        [SerializeField, Child(Flag.ExcludeSelf)] private TMP_Text count;
        [SerializeField, Self] private FollowMouseUI followMouse;

        private void Awake()
        {
            followMouse.enabled = false;
        }

        private void Update()
        {
            if (anchor == null) return;
            transform.position = anchor.position;
        }

        public void Set(ItemStack itemStack, Vector2 cellsize)
        {
            ItemStackStored = itemStack;

            if (itemStack == null)
            {
                icon.color = Color.clear;
                count.text = string.Empty;
                return;
            }

            icon.color = Color.white;
            icon.sprite = itemStack.itemType.Icon;
            count.text = itemStack.itemType.StackSize > 1 ? itemStack.amount.ToString() : "";

            var rect = (RectTransform)transform;
            rect.sizeDelta = cellsize * new Vector2(itemStack.itemType.Size.x, itemStack.itemType.Size.y);
        }

        public void RefreshAmount()
        {
            if (ItemStackStored == null)
            {
                Debug.LogWarning("Tried to refresh amount but ItemStack stored was null");
                return;
            }

            count.text = ItemStackStored.itemType.StackSize > 1 ? ItemStackStored.amount.ToString() : "";
        }

        public void AnchorTo(RectTransform t)
        {
            followMouse.enabled = false;
            transform.position = t.position;
            anchor = t;
        }

        public void FollowMouse()
        {
            followMouse.enabled = true;
        }
    }
}