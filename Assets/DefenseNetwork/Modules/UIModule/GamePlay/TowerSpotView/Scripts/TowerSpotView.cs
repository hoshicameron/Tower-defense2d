using System.Collections.Generic;
using System.Linq;
using DefenseNetwork.Core.EventChannels.DataObjects.Enums;
using DefenseNetwork.CoreTowerDefense.Requests;
using GameSystemsCookbook;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Utilities;

namespace DefenseNetwork.Modules.UIModule.GamePlay.TowerSpotView.Scripts
{
    public class TowerSpotView : MonoBehaviour
    {
        [Header("Event Channel")]
        [SerializeField] private GameObjectEventChannelSO towerSelectionChannel;
        [SerializeField] private TowerDataRequestEventChannelSO towerDataRequestEventChannel;
        
        [Space] [Header("UI")]
        [SerializeField] private CanvasGroup towerSpotCanvas;
        [SerializeField] private RectTransform towerDeployPanel;
        [SerializeField] private Button closeButton;
        [SerializeField] private TowerDeployButton towerDeployButtonPrefab;
        
        [Space] [Header("Events")] 
        public UnityEvent<int> onBulletTowerDeployButtonClicked;
        public UnityEvent<int> onMissileTowerDeployButtonClicked;

        private List<ITowerData> towerDatas;
        private void OnEnable()
        {
            towerSelectionChannel.OnEventRaised += TowerSelected;
        }

        private void OnDisable()
        {
            towerSelectionChannel.OnEventRaised -= TowerSelected;
        }
        
        private void TowerSelected(GameObject selectedGameObject)
        {
            if(selectedGameObject != gameObject)
                HideTowerSpotPanel();
        }

        private void Start()
        {
            HideTowerSpotPanel();
            closeButton.onClick.AddListener(HideTowerSpotPanel);
            var towerDataRequest = new TowerDataRequest();
            towerDataRequest.OnRequestResult += UpdateUI;
            towerDataRequestEventChannel.RaiseEvent(towerDataRequest);
        }

        private void UpdateUI(List<ITowerData> availableTowers)
        {
            towerDatas = availableTowers;
            towerDeployPanel.DestroyAllChildren();
            foreach (var towerData in towerDatas)
            {
                var deployButton = Instantiate(towerDeployButtonPrefab, towerDeployPanel, false);
                deployButton.UpdateUI(towerData.Sprite, towerData.DeployCost, towerData.Type);
                deployButton.onClick += DeployButtonClicked;
            }
        }

        private void DeployButtonClicked(TowerType towerType)
        {
            var deployCost = towerDatas.FirstOrDefault(data => data.Type == towerType)!.DeployCost;
            switch (towerType)
            {
                case TowerType.BulletTower:
                    onBulletTowerDeployButtonClicked?.Invoke(deployCost);
                    break;
                case TowerType.MissileTower:
                    onMissileTowerDeployButtonClicked?.Invoke(deployCost);
                    break;
            }
        }

        private void HideTowerSpotPanel()
        {
            towerSpotCanvas.alpha = 0;
            towerSpotCanvas.interactable = false;
            towerSpotCanvas.blocksRaycasts = false;
        }

        public void ShowSpotTowerPanel()
        {
            towerSpotCanvas.alpha = 1;
            towerSpotCanvas.interactable = true;
            towerSpotCanvas.blocksRaycasts = true;
            towerSelectionChannel.RaiseEvent(gameObject);
        }
    }
}