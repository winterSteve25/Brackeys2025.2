using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Objects.UpgradeStation
{
    public class UpgradeUI : MonoBehaviour
    {
        [SerializeField] private RectTransform container;
        [SerializeField] private UpgradeItem prefab;
        [SerializeField] private TMP_Text sellOrBuyText;

        public void Initialize(List<Upgrade> upgrades)
        {
            for (int i = 0; i < container.transform.childCount; i++)
            {
                Destroy(container.transform.GetChild(i).gameObject);
            }
            
            foreach (var x in upgrades)
            {
                var upgrade = Instantiate(prefab, container);
                upgrade.Initialize(x, sellOrBuyText, upgrades);
            }
            
            LayoutRebuilder.ForceRebuildLayoutImmediate(container);
        }
    }
}