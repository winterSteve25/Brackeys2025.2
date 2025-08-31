using System;
using Audio;
using FMOD.Studio;
using UnityEngine;
using World;

namespace Items
{
    public class MiningLaserItem : MiningToolItem
    {
        private bool _wasMining;
        private EventInstance _instance;

        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private Transform head;

        protected override void Awake()
        {
            base.Awake();
            lineRenderer.gameObject.SetActive(false);
            _instance = AudioManager.CreateInstance(FModEvents.Instance.LaserUse);
        }

        private void OnDestroy()
        {
            _instance.stop(STOP_MODE.IMMEDIATE);
            _instance.release();
        }

        private void Update()
        {
            if (_wasMining && !isMining)
            {
                lineRenderer.gameObject.SetActive(false);
                _instance.stop(STOP_MODE.ALLOWFADEOUT);
            }
            
            if (!_wasMining && isMining)
            {
                lineRenderer.gameObject.SetActive(true);
                _instance.start();
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