using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utils;

namespace Objects.UpgradeStation
{
    public class UpgradeItem : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text itemCost;
        [SerializeField] private Color buyColor;
        [SerializeField] private TMP_Text sellOrBuyTxt;

        [SerializeField] private Image background;
        [SerializeField] private Sprite nonHoverSprite;
        [SerializeField] private Sprite hoverSprite;

        private Upgrade _upgrade;
        private List<Upgrade> _upgradesForToday;

        public void Initialize(Upgrade upgrade, TMP_Text sellOrBuyAmount, List<Upgrade> upgradesForToday)
        {
            _upgrade = upgrade;
            _upgradesForToday = upgradesForToday;
            
            icon.sprite = upgrade.Icon;
            itemCost.text = upgrade.Cost.ToString();
            sellOrBuyTxt = sellOrBuyAmount;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (CarryOverDataManager.Instance.Gold < _upgrade.Cost) return;
            CarryOverDataManager.Instance.Gold -= _upgrade.Cost;
            _upgrade.Apply();
            _upgradesForToday.Remove(_upgrade);
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform) transform.parent);
            Destroy(gameObject);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            sellOrBuyTxt.color = buyColor;
            sellOrBuyTxt.text = $"-{_upgrade.Cost}";
            background.sprite = hoverSprite;
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)sellOrBuyTxt.transform.parent);
            
            ToolTipManager.Instance.Show(_upgrade.Name, _upgrade.Description);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            sellOrBuyTxt.text = "";
            background.sprite = nonHoverSprite;
            
            ToolTipManager.Instance.Hide();
        }
    }
}