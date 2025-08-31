using System;
using System.Collections.Generic;
using ED.SC;
using InventorySystem;
using Objects.UpgradeStation;
using UnityEngine;
using UnityEngine.Serialization;
using World;

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
        public float oxygenDepletionMultiplier = 1;
        public float fallDamageMultiplier = 1;
        public float oreSpawnRateMultiplier = 1;
        public float shopItemPriceMultiplier = 1;
        public float targetShopItemPriceMultiplier = 1;

        private static int _day;
        public static int Day => _day / 2;

        public static bool justDied = false;
        public static int goldAfterDeath;

        private void Awake()
        {
            if (justDied)
            {
                Gold = goldAfterDeath;
                justDied = false;
                _day = 0;
                // todo this is scuffed
                UpgradeStationUIController.appearedBefore.Clear();
            }
            
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
                return;
            }

            Instance.shopItemPriceMultiplier = targetShopItemPriceMultiplier;
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