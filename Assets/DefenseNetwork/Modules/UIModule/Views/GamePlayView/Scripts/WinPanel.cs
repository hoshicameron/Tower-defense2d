using DefenseNetwork.CoreTowerDefense.Enums;
using DefenseNetwork.CoreTowerDefense.ScriptableObjects;
using GameSystemsCookbook;
using UnityEngine;
using UnityEngine.UI;

namespace DefenseNetwork.Modules.UIModule.Views.GamePlayView.Scripts
{
    public class WinPanel : HidablePanel
    {
        [Space] [Header("Event Channel")] 
        [SerializeField] private GameStateEventChannelSO gameStateEventChannel;
        [SerializeField] private VoidEventChannelSO exitToMenuEventChannel;
        [SerializeField] private VoidEventChannelSO continueGameEventChannel;
            
        [Space] [Header("Components")] 
        [SerializeField] private Button continueButton;
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
            if(state == GameState.Won)   Show();
        }

        private void Initialize()
        {
            Hide();
            continueButton.onClick.AddListener(() => continueGameEventChannel.RaiseEvent());
            exitButton.onClick.AddListener(()=>exitToMenuEventChannel.RaiseEvent() );
        }
    }
}