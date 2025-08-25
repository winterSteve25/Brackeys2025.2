using System.Collections.Generic;
using KBCore.Refs;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem.UI
{
    public class TetrisInventoryGridUI : MonoBehaviour
    {
        [Header("References")]
        [SerializeField, Child] protected GridLayoutGroup container;

        [Header("Parameters")]
        [SerializeField] private int startingRow;
        [SerializeField, Tooltip("Set to -1 for to the end")] private int lastRow;

        public void InitializeSlots(Dictionary<Vector2Int, TetrisSlot> slots, Vector2 cellSize, int width,
            int height, TetrisSlot slotPrefab, TetrisInventoryUI master)
        {
            container.cellSize = cellSize;
            if (lastRow == -1)
            {
                lastRow = height;
            }

            ((RectTransform)transform).sizeDelta = new Vector2(
                width * cellSize.x + container.padding.left + container.padding.right,
                (lastRow - startingRow) * cellSize.y + container.padding.top + container.padding.bottom
            );

            for (int j = startingRow; j < lastRow; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    var slot = Instantiate(slotPrefab, container.transform);
                    var vector2Int = new Vector2Int(i, j);
                    slot.Initialize(vector2Int, master);
                    slots[vector2Int] = slot;
                }
            }

            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)transform);
        }
    }
}