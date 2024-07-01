using GameSystemsCookbook;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DefenseNetwork.Modules.UIModule.GamePlay.TowerView.Scripts
{
    public class TowerView : MonoBehaviour
    {
        [Header("Event Channel")]
        [SerializeField] private GameObjectEventChannelSO towerSelectionChannel;
        [Header("UI")]
        [SerializeField] private CanvasGroup towerSpotCanvas;
        [SerializeField] private Button closeButton;
        [SerializeField] private Button sellButton;
        [SerializeField] private UpgradeButton upgradeButton;
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
            sellButton.onClick.AddListener(SellButtonClicked);
            upgradeButton.onClick += UpgradeButtonClicked;
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

        private void HideTowerPanel()
        {
            towerSpotCanvas.alpha = 0;
            towerSpotCanvas.interactable = false;
            towerSpotCanvas.blocksRaycasts = false;
        }

        private void ShowTowerPanel()
        {
            towerSpotCanvas.alpha = 1;
            towerSpotCanvas.interactable = true;
            towerSpotCanvas.blocksRaycasts = true;
            towerSelectionChannel.RaiseEvent(gameObject);
        }

        public void ShowTowerPanelWithUpgrade(int upgradeCost)
        {
            ShowTowerPanel();
            upgradeButton.SetText(upgradeCost.ToString());
        }

        public void ShowTowerPanelWithoutUpgrade()
        {
            ShowTowerPanel();
            upgradeButton.gameObject.SetActive(false);
        }

        public void SetUpgradeButtonInteractable(bool interactable) => upgradeButton.Interactable(interactable);

    }
}