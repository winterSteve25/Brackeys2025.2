using Audio;
using DeathMenu;
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
        [SerializeField] private DeathScreen deathScreen;

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
            Tween.Custom(health + damage, health, 0.4f, x => healthBar.UpdateValue(x), Ease.OutCubic, useUnscaledTime: true);

            if (health <= 0)
            {
                Time.timeScale = 0;
                deathScreen.Show();
            }
            
            AudioManager.PlayOnce(FModEvents.Instance.PlayerFall, transform.position);
        }

        public void Heal(float damage)
        {
            health += damage;
            Tween.Custom(health - damage, health, 0.4f, x => healthBar.UpdateValue(x), Ease.OutCubic, useUnscaledTime: true);
        }
    }
}