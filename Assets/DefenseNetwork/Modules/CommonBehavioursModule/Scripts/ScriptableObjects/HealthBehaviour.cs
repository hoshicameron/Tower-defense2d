using System;
using UnityEngine;

namespace DefenseNetwork.Modules.CommonBehavioursModule.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "HealthBehaviour", menuName = "Gameplay/Behaviours/HealthBehaviour")]
    public class HealthBehaviour : ScriptableObject
    {
        public event Action<int, int> OnHealthChanged;
        public event Action OnDeath;

        private int currentHealth;

        public int CurrentHealth
        {
            get => currentHealth;
            set
            {
                currentHealth = value;
                OnHealthChanged?.Invoke(currentHealth,maxHealth);
            }
        }
        private int maxHealth;
        public void Initialize(int health)
        {
            maxHealth = health;
            CurrentHealth = health;
        }

        public void TakeDamage(int damage)
        {
            CurrentHealth = Mathf.Max(currentHealth - damage, 0);
            
            if (currentHealth == 0)
            {
                OnDeath?.Invoke();
            }
        }

        public void Heal(int amount)
        {
            CurrentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        }

        public float GetHealthPercentage()
        {
            return (float)CurrentHealth / maxHealth;
        }
    }
}