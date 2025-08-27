using PrimeTween;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem.UI
{
    public class HotbarSlot : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TMP_Text countText;
        [SerializeField] private Transform child;

        [SerializeField] private float zoomFactor = 1.25f;
        [SerializeField] private float transitionTime = 0.2f;
        [SerializeField] private Ease easeIn;
        [SerializeField] private Ease easeOut;

        public void Initialize(ItemStack item)
        {
            image.sprite = item.itemType.Icon;
            image.GetComponent<AspectRatioFitter>().aspectRatio = item.itemType.Size.x / (float) item.itemType.Size.y;

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
    }
}