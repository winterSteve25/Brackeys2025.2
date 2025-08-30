using System;
using PrimeTween;
using TMPro;
using UnityEngine;
using Utils;

namespace DeathMenu
{
    public class DeathScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup group;
        [SerializeField] private TMP_Text goldLost;

        private void Awake()
        {
            group.alpha = 0;
            group.interactable = false;
            group.blocksRaycasts = false; 
        }

        public void Show()
        {
            CarryOverDataManager.justDied = true;
            CarryOverDataManager.goldAfterDeath = CarryOverDataManager.Instance.Gold / 2;
        
            goldLost.text = $"You lost <sprite index=0> {CarryOverDataManager.goldAfterDeath}";
            group.interactable = true;
            group.blocksRaycasts = true;
            Tween.Alpha(group, 1, 0.2f, useUnscaledTime: true);
        
            Destroy(CarryOverDataManager.Instance.gameObject);
        }
    }
}