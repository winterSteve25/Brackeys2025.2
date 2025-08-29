using KBCore.Refs;
using UnityEngine;
using UnityEngine.UI;

namespace Utils
{
    [ExecuteAlways]
    public class UIRestricted : ValidatedMonoBehaviour
    {
        [SerializeField] private LayoutElement layoutElement;
        [SerializeField, Self] private RectTransform thisRect;

        private bool _wasEnabled;
        
        private void Update()
        {
            layoutElement.enabled = thisRect.sizeDelta.x > layoutElement.preferredWidth ||
                                    Mathf.Approximately(thisRect.sizeDelta.x, layoutElement.preferredWidth);

            if (_wasEnabled && !layoutElement.enabled)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(thisRect);
            }
            
            _wasEnabled = layoutElement.enabled;
        }
    }
}