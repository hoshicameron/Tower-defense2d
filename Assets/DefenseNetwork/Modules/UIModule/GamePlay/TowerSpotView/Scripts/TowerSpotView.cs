using DefenseNetwork.Modules.UIModule.GamePlay.TowerSpotView.Scripts.ScriptableObjects;
using GameSystemsCookbook;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace DefenseNetwork.Modules.UIModule.GamePlay.TowerSpotView.Scripts
{
    public class TowerSpotView : MonoBehaviour
    {
        [Header("Event Channel")]
        [SerializeField] private GameObjectEventChannelSO towerSelectionChannel;
        
        [Space]
        [Header("UI")]
        [SerializeField] private CanvasGroup towerSpotCanvas;
        [SerializeField] private RectTransform towerDeployPanel;
        [SerializeField] private Button closeButton;
        [SerializeField] private TowerDeployButton towerDeployButtonPrefab;
        [Space]
        [Header("Data")] 
        [SerializeField] private TowerSpotDataSO towerSpotDataSo;

        [Space] [Header("Events")] 
        public UnityEvent<int, int> onTowerDeployButtonClicked;
        

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
            DestroyAllChildren(towerDeployPanel);
            InstantiateTowerDeployButtons();
        }

        private void InstantiateTowerDeployButtons()
        {
            foreach (var towerData in towerSpotDataSo.AvailableTowers)
            {
                var deployButton = Instantiate(towerDeployButtonPrefab, towerDeployPanel, false);
                deployButton.UpdateUI(towerData.TowerSprite, towerData.DeployCost, towerData.Type);
                deployButton.onClick += DeployButtonClicked;
            }
        }

        private void DeployButtonClicked(int towerType, int cost)
        {
            onTowerDeployButtonClicked?.Invoke(towerType,cost);
        }

        private void DestroyAllChildren(RectTransform parent)
        {
            for (var i = parent.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(parent.transform.GetChild(i).gameObject);
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