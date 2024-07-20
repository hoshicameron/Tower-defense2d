using System;
using UnityEngine;
using UnityEngine.UI;

namespace DefenseNetwork.Modules.UIModule.GamePlay.EnemyHealthView.Scripts
{
    public class EnemyHealthView : MonoBehaviour
    {
        [SerializeField] private Slider healthSlider;
        
        public void SetHealthSlider(int currentHealth, int maxHealth)
        {
            healthSlider.value = (float)currentHealth/maxHealth;
        }
    }
}