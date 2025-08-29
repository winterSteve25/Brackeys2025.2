using System;
using UnityEngine;
using World;

namespace Items
{
    public class MiningLaserItem : MiningToolItem
    {
        private bool _wasMining;

        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private Transform head;

        protected override void Awake()
        {
            base.Awake();
            lineRenderer.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (_wasMining && !isMining)
            {
                lineRenderer.gameObject.SetActive(false);
            }
            
            if (!_wasMining && isMining)
            {
                lineRenderer.gameObject.SetActive(true);
            }
            
            if (isMining)
            {
                lineRenderer.SetPosition(0, head.position);
                lineRenderer.SetPosition(1, WorldManager.Current.CellToWorld(wasMiningPosition) + new Vector2(0.5f, 0.5f));
            }
            
            _wasMining = isMining;
        }
    }
}