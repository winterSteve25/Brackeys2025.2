using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace InventorySystem.UI
{
    public class TetrisSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [Header("Debug Info DO NOT EDIT")]
        [SerializeField] private Vector2Int position;
        [SerializeField] private TetrisInventoryUI inventory;

        [Header("References")]
        [SerializeField] private Image background;
        [SerializeField] private Sprite nonHoverImage;
        [SerializeField] private Sprite hoverImage;
        
        public void Initialize(Vector2Int pos, TetrisInventoryUI inv)
        {
            position = pos;
            inventory = inv;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            background.sprite = hoverImage;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            background.sprite = nonHoverImage;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            inventory.SlotClicked(position);
        }
    }
}