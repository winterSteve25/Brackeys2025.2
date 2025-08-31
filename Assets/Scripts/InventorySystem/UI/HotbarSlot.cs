using Player;
using PrimeTween;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utils;

namespace InventorySystem.UI
{
    public class HotbarSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] private Image image;
        [SerializeField] private TMP_Text countText;
        [SerializeField] private Transform child;

        [SerializeField] private PlayerInventory inventory;
        [SerializeField] private ItemStack itemStored;
        [SerializeField] private int index;

        [SerializeField] private float zoomFactor = 1.25f;
        [SerializeField] private float transitionTime = 0.2f;
        [SerializeField] private Ease easeIn;
        [SerializeField] private Ease easeOut;

        public void Initialize(ItemStack item, PlayerInventory inv, int i)
        {
            image.sprite = item.itemType.Icon;
            image.GetComponent<AspectRatioFitter>().aspectRatio = item.itemType.Size.x / (float)item.itemType.Size.y;

            inventory = inv;
            itemStored = item;

            if (i != -1)
            {
                index = i;
            }

            if (item.itemType.StackSize > 1)
            {
                countText.text = item.amount.ToString();
            }
        }

        public void Select()
        {
            Tween.StopAll(child);
            Tween.Scale(child, new Vector3(zoomFactor, zoomFactor, 1), transitionTime, ease: easeIn);
        }

        public void Unselect()
        {
            Tween.StopAll(child);
            Tween.Scale(child, Vector3.one, transitionTime, ease: easeOut);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            ToolTipManager.Instance.Show(itemStored.itemType.Name, "", false);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ToolTipManager.Instance.Hide();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            // inventory.SetSelectedItem(index);
        }
    }
}