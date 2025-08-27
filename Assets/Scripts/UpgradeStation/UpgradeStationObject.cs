using Player;
using UnityEngine;

namespace UpgradeStation
{
    public class UpgradeStationObject : MonoBehaviour
    {
        [SerializeField] private UpgradeStationUIController ui;
        
        public void Open(PlayerInventory inventory)
        {
            ui.Show(inventory.Inventory);
        }
    }
}