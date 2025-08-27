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
        private GameObject _visualSelected;

        [Header("References")]
        [SerializeField, Self] private PlayerInventory inventory;
        [SerializeField] private Transform handAnchor;
        [SerializeField] private GameObject defaultPrefab;

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
            if (_visualSelected == null) return;

            var mp = UpdatableItem.GetMousePosInWorld();
            mp -= (Vector2)handAnchor.position;

            var sign = Mathf.Sign(handAnchor.localScale.x);
            var angle = Mathf.Atan2(sign * mp.y, sign * mp.x) * Mathf.Rad2Deg;

            handAnchor.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            if (Mathf.Abs(angle) > 90)
            {
                var scale = handAnchor.localScale;
                scale.x *= -1;
                handAnchor.localScale = scale;
            }

            if (!usableItems) return;
            if (_itemSelected == null) return;
            _itemSelected.UseTick(inventory);
        }

        private void InventoryOnOnSelectedItemChanged(ItemStack obj)
        {
            if (_visualSelected != null)
            {
                Destroy(_visualSelected.gameObject);
            }

            if (obj == null) return;

            var visualPrefab = obj.itemType.VisualPrefab;
            if (visualPrefab == null)
            {
                visualPrefab = defaultPrefab;
            }

            var visuals = Instantiate(visualPrefab, handAnchor);
            if (visuals.TryGetComponent(out DefaultItem defaultItem))
            {
                defaultItem.Initialize(obj.itemType.Icon);
            }

            _visualSelected = visuals;

            if (visuals.TryGetComponent(out UpdatableItem updatableItem))
            {
                _itemSelected = updatableItem;
                updatableItem.item = obj;
                updatableItem.handAnchor = handAnchor;
                return;
            }

            if (obj.itemType.LogicPrefab == null) return;
            var logic = Instantiate(obj.itemType.LogicPrefab, _visualSelected.transform);
            logic.item = obj;
            logic.handAnchor = handAnchor;
            _itemSelected = logic;
        }
    }
}