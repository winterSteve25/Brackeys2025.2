using System;
using InventorySystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace Utils
{
    public class CarryOverDataManager : MonoBehaviour
    {
        public static CarryOverDataManager Instance { get; private set; }

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
        
        public event Action<int> OnGoldChanged;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
                return;
            }
            
            Destroy(gameObject);
        }
    }
}