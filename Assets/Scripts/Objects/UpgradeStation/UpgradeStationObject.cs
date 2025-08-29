using Audio;
using Player;
using UnityEngine;

namespace Objects.UpgradeStation
{
    public class UpgradeStationObject : MonoBehaviour
    {
        [SerializeField] private UpgradeStationUIController ui;
        
        public void Open(PlayerInventory inventory)
        {
            ui.Show(inventory.Inventory);
            AudioManager.PlayOnce(FModEvents.Instance.TerminalInteract, transform.position);
        }
    }
}