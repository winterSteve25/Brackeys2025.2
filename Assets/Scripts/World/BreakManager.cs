using System.Collections.Generic;
using KBCore.Refs;
using TMPro;
using UnityEngine;
using Utils;

namespace World
{
    public class BreakManager : MonoBehaviour
    {
        public static BreakManager Current { get; private set; }
        
        [Header("References")]
        [SerializeField] private TMP_Text prefab;
        [SerializeField, Anywhere] private Canvas parent;
        [SerializeField, Anywhere] private WorldManager worldManager;
        [SerializeField, Anywhere] private ItemEntityManager itemEntityManager;

        private Dictionary<Vector2Int, BreakProgress> _breakingProgress;

        private void Awake()
        {
            _breakingProgress = new Dictionary<Vector2Int, BreakProgress>();
            Current = this;
        }

        public bool TickBreak(Vector2Int pos, float totalTime)
        {
            if (!_breakingProgress.ContainsKey(pos))
            {
                _breakingProgress[pos] = new BreakProgress(0, Instantiate(prefab, parent.transform));
            }

            var breakProgress = _breakingProgress[pos];
            breakProgress.Progress += Time.deltaTime / totalTime;
            breakProgress.Text.text = breakProgress.Progress.ToString("0.00");
            breakProgress.Text.transform.position = MainCamera.Current.WorldToScreenPoint(
                worldManager.CellToWorld(pos)
            );

            if (!(breakProgress.Progress >= 1)) return false;
            
            CancelBreak(pos);
            CompleteBreak(pos);

            worldManager.RemoveTile(pos);
            return true;
        }

        public void CompleteBreak(Vector2Int pos)
        {
            if (!worldManager.TryGetTile(pos, out var tile)) return;
            var loot = tile.Material.Loot;
            if (loot == null) return;
            
            foreach (var l in loot)
            {
                itemEntityManager.SpawnApproximatelyAt(worldManager.CellToWorld(pos) + new Vector2(0.5f, 0.5f), l);
            }
        }

        public void CancelBreak(Vector2Int pos)
        {
            if (!_breakingProgress.TryGetValue(pos, out var breakProgress)) return;
            Destroy(breakProgress.Text.gameObject);
            _breakingProgress.Remove(pos);
        }

        private class BreakProgress
        {
            public float Progress;
            public TMP_Text Text;

            public BreakProgress(float progress, TMP_Text text)
            {
                Progress = progress;
                Text = text;
            }
        }
    }
}