using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefenseNetwork.Modules.UIModule.GamePlay.TowerView.Scripts
{
    [RequireComponent(typeof(Button))]
    public class UpgradeButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI upgradeCostText;
        
        private Button button;

        public event Action onClick;

        private void Awake()
        {
            button = GetComponent<Button>();
        }

        private void Start()
        {
            button.onClick.AddListener(() => onClick?.Invoke());
        }

        public void SetText(string upgradeCost) => upgradeCostText.SetText(upgradeCost);
        public void Interactable(bool interactable) => button.interactable = interactable;
    }
}