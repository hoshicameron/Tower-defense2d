using GameSystemsCookbook;
using UnityEngine;
using UnityEngine.UI;

namespace DefenseNetwork.Modules.UIModule.Views.GamePlayView.Scripts
{
    public class LosePanel : HidablePanel
    {
        [Space] [Header("Event Channel")] 
        [SerializeField] private VoidEventChannelSO loseChannel;
        [SerializeField] private VoidEventChannelSO exitToMenuEventChannel;
        [SerializeField] private VoidEventChannelSO restartLevelEventChannel;
        
        [Space] [Header("Components")] 
        [SerializeField] private Button retryButton;
        [SerializeField] private Button exitButton;
        
        
        private void OnEnable()
        {
            Initialize();
            loseChannel.OnEventRaised += Show;
        }

        private void OnDisable()
        {
            loseChannel.OnEventRaised -= Show;
        }

        private void Initialize()
        {
            Hide();
            retryButton.onClick.AddListener(() => restartLevelEventChannel.RaiseEvent());
            exitButton.onClick.AddListener(()=> exitToMenuEventChannel.RaiseEvent());
        }
    }
}