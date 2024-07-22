using DefenseNetwork.CoreTowerDefense.Enums;
using DefenseNetwork.CoreTowerDefense.ScriptableObjects;
using GameSystemsCookbook;
using UnityEngine;
using UnityEngine.UI;

namespace DefenseNetwork.Modules.UIModule.Views.GamePlayView.Scripts
{
    public class LosePanel : HidablePanel
    {
        [Space] [Header("Event Channel")] 
        [SerializeField] private GameStateEventChannelSO gameStateEventChannel;
        [SerializeField] private VoidEventChannelSO exitToMenuEventChannel;
        [SerializeField] private VoidEventChannelSO restartLevelEventChannel;
        
        [Space] [Header("Components")] 
        [SerializeField] private Button retryButton;
        [SerializeField] private Button exitButton;
        
        
        private void OnEnable()
        {
            Initialize();
            gameStateEventChannel.OnEventRaised += HandleGameStateChange;
        }

        private void OnDisable()
        {
            gameStateEventChannel.OnEventRaised -= HandleGameStateChange;
        }

        private void HandleGameStateChange(GameState state)
        {
            if(state == GameState.Lost)   Show();
        }
        private void Initialize()
        {
            Hide();
            retryButton.onClick.AddListener(() => restartLevelEventChannel.RaiseEvent());
            exitButton.onClick.AddListener(()=> exitToMenuEventChannel.RaiseEvent());
        }
    }
}