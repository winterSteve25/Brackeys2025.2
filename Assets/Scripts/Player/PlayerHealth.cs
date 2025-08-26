using System;
using TMPro;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private float health;
        [SerializeField] private TMP_Text healthText;

        private void Awake()
        {
            healthText.text = "Hp: " + health.ToString("F1");
        }

        public void TakeDamage(float damage)
        {
            health -= damage;
            healthText.text = "Hp: " + health.ToString("F1");
        }

        public void Heal(float damage)
        {
            health += damage;
            healthText.text = "Hp: " + health.ToString("F1");
        }
    }
}