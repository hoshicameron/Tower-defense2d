using GameSystemsCookbook;
using UnityEngine;
using UnityEngine.UI;

namespace DefenseNetwork.Modules.UIModule.Views.GamePlayView.Scripts
{
    public class WinPanel : HidablePanel
    {
        [Space] [Header("Event Channel")] 
        [SerializeField] private VoidEventChannelSO winChannel;
        [SerializeField] private VoidEventChannelSO exitToMenuEventChannel;
        [SerializeField] private VoidEventChannelSO continueGameEventChannel;
            
        [Space] [Header("Components")] 
        [SerializeField] private Button continueButton;
        [SerializeField] private Button exitButton;
        
        private void OnEnable()
        {
            Initialize();
            winChannel.OnEventRaised += Show;
        }

        private void OnDisable()
        {
            winChannel.OnEventRaised -= Show;
        }

        private void Initialize()
        {
            Hide();
            continueButton.onClick.AddListener(() => continueGameEventChannel.RaiseEvent());
            exitButton.onClick.AddListener(()=>exitToMenuEventChannel.RaiseEvent() );
        }
    }
}