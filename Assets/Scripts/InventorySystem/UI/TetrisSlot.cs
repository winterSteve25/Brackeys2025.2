using UnityEngine;
using UnityEngine.EventSystems;

namespace InventorySystem.UI
{
    public class TetrisSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [Header("Debug Info DO NOT EDIT")]
        [SerializeField] private Vector2Int position;
        [SerializeField] private TetrisInventoryUI inventory;
        
        public void Initialize(Vector2Int pos, TetrisInventoryUI inv)
        {
            position = pos;
            inventory = inv;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
        }

        public void OnPointerExit(PointerEventData eventData)
        {
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            inventory.SlotClicked(position);
        }
    }
}