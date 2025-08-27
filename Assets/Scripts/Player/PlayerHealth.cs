using KBCore.Refs;
using TMPro;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : ValidatedMonoBehaviour, IHealthComponent
    {
        [SerializeField] private float health;
        [SerializeField] private TMP_Text healthText;
        [SerializeField, Self] private PlayerAnimation animations;

        private void Awake()
        {
            healthText.text = "Hp: " + health.ToString("F1");
        }

        public void TakeDamage(float damage)
        {
            if (damage <= 0) return;
            
            health -= damage;
            healthText.text = "Hp: " + health.ToString("F1");
            animations.Hurt();
        }

        public void Heal(float damage)
        {
            health += damage;
            healthText.text = "Hp: " + health.ToString("F1");
        }
    }
}