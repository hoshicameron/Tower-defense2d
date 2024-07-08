using System;
using UnityEngine;
using UnityEngine.UI;

namespace DefenseNetwork.Modules.UIModule.GamePlay.EnemyHealthView.Scripts
{
    [RequireComponent(typeof(Image))]
    public class HealthBarColor : MonoBehaviour
    {
        [Header("Colors")]
        [SerializeField] private Color fullHealthColor = Color.green; 
        [SerializeField] private Color mediumHealthColor = Color.yellow; 
        [SerializeField] private Color lowHealthColor = Color.red; 

        private Image healthSliderImage;

        private void Awake()
        {
            healthSliderImage = GetComponent<Image>();
        }

        public void SetColor(float health)
        {
            healthSliderImage.color = health switch
            {
                > 0.8f => fullHealthColor,
                > 0.3f => mediumHealthColor,
                _ => lowHealthColor
            };
        }
    }
}