using InventorySystem;
using UnityEngine;

namespace Utils
{
    public class CarryOverDataManager : MonoBehaviour
    {
        public static CarryOverDataManager Instance { get; private set; }

        public int gold;
        public TetrisInventory backpack;

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