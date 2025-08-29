using System;
using ED.SC;
using InventorySystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace Utils
{
    public class CarryOverDataManager : MonoBehaviour
    {
        public static CarryOverDataManager Instance { get; private set; }
        public event Action<int> OnGoldChanged;

        [SerializeField] private int gold;
        public int Gold
        {
            get => gold;
            set
            {
                gold = value;
                OnGoldChanged?.Invoke(value);
            }
        }

        [field: SerializeField, FormerlySerializedAs("inventory")]
        public TetrisInventory Inventory { get; private set; }

        private static int _day;
        public static int Day => _day / 2;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
                return;
            }

            _day++;
            Destroy(gameObject);
        }

        [Command]
        private void AddGold(int amount)
        {
            Gold += amount;
        }
    }
}