using KBCore.Refs;
using TMPro;
using UnityEngine;
using Utils;

namespace Player
{
    public class PlayerOxygenMeter : ValidatedMonoBehaviour
    {
        [Header("Debug Info DO NOT EDIT")]
        [SerializeField] private float timer;
        
        [Header("References")]
        [SerializeField] private MakeshiftProgressBar oxygenBar;
        [SerializeField, Self] private PlayerHealth health;
        
        [Header("Parameters")]
        [SerializeField] private float oxygen;
        [SerializeField] private float oxygenDecreaseRate;
        [SerializeField] private float damagePerTick;
        [SerializeField] private float tickRate;

        private void Update()
        {
            oxygen -= oxygenDecreaseRate * Time.deltaTime;

            if (oxygen <= 0)
            {
                oxygen = 0;
                timer += Time.deltaTime;

                if (timer >= tickRate)
                {
                    timer = 0;
                    health.TakeDamage(damagePerTick);
                }
            }
            
            oxygenBar.UpdateValue(oxygen);
        }
    }
}