using KBCore.Refs;
using PrimeTween;
using UnityEngine;
using Utils;

namespace Player
{
    public class PlayerHealth : ValidatedMonoBehaviour, IHealthComponent
    {
        [SerializeField] private float health;
        [SerializeField] private MakeshiftProgressBar healthBar;
        [SerializeField, Self] private PlayerAnimation animations;

        private void Start()
        {
            healthBar.UpdateValue(1);
            healthBar.Initialize(health);
        }

        public void TakeDamage(float damage)
        {
            if (damage <= 0) return;
            
            health -= damage;
            animations.Hurt();
            CameraEffects.Current.trauma += 0.25f;
            Tween.Custom(health + damage, health, 0.4f, x => healthBar.UpdateValue(x), Ease.OutCubic);
        }

        public void Heal(float damage)
        {
            health += damage;
            Tween.Custom(health - damage, health, 0.4f, x => healthBar.UpdateValue(x), Ease.OutCubic);
        }
    }
}