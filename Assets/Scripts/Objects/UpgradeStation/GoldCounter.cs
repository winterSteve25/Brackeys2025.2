using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Objects.UpgradeStation
{
    public class GoldCounter : MonoBehaviour
    {
        [SerializeField] private TMP_Text amountTxt;

        private void Start()
        {
            CarryOverDataManager.Instance.OnGoldChanged += InstanceOnOnGoldChanged;
            InstanceOnOnGoldChanged(CarryOverDataManager.Instance.Gold);
        }

        private void OnDestroy()
        {
            CarryOverDataManager.Instance.OnGoldChanged -= InstanceOnOnGoldChanged;
        }

        private void InstanceOnOnGoldChanged(int obj)
        {
            amountTxt.text = obj.ToString();
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)amountTxt.transform.parent);
        }
    }
}