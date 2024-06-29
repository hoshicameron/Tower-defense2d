using GameSystemsCookbook;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Modules.TowerModule.SubModules.UIModule.Scripts
{
    public class TowerView : MonoBehaviour
    {
        [Header("Event Channel")]
        [SerializeField] private GameObjectEventChannelSO towerSelectionChannel;
        [Header("UI")]
        [SerializeField] private RectTransform towerPanel;
        [SerializeField] private Button closeButton;
        [SerializeField] private Button upgradeButton;
        [SerializeField] private Button sellButton;
        [SerializeField] private TextMeshProUGUI upgradeCostText;
        [Space]
        [SerializeField] public UnityEvent onUpgradeButtonPressed;
        [SerializeField] public UnityEvent onSellButtonPressed;

        private void OnEnable()
        {
            towerSelectionChannel.OnEventRaised += TowerSelected;
        }

        private void OnDisable()
        {
            towerSelectionChannel.OnEventRaised -= TowerSelected;
        }

        private void Start()
        {
            HideTowerPanel();
            closeButton.onClick.AddListener(HideTowerPanel);
            upgradeButton.onClick.AddListener(UpgradeButtonClicked);
            sellButton.onClick.AddListener(SellButtonClicked);
        }

        private void TowerSelected(GameObject selectedGameObject)
        {
            if(selectedGameObject != gameObject)
                HideTowerPanel();
        }

        private void SellButtonClicked()
        {
            onSellButtonPressed?.Invoke();
            HideTowerPanel();
        }

        private void UpgradeButtonClicked()
        {
            onUpgradeButtonPressed?.Invoke();
            HideTowerPanel();
        }

        public void HideTowerPanel() => towerPanel.gameObject.SetActive(false);
        public void ShowTowerPanel()
        {
            towerPanel.gameObject.SetActive(true);
            towerSelectionChannel.RaiseEvent(gameObject);
        }

        public void ShowTowerPanelWithUpgrade(int upgradeCost)
        {
            ShowTowerPanel();
            upgradeButton.gameObject.SetActive(true);
            upgradeCostText.SetText(upgradeCost.ToString());
        }

        public void ShowTowerPanelWithoutUpgrade()
        {
            ShowTowerPanel();
            upgradeButton.gameObject.SetActive(false);
        }
    }
}