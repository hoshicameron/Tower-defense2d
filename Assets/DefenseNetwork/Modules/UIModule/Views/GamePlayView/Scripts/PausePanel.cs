using GameSystemsCookbook;
using UnityEngine;
using UnityEngine.UI;

namespace DefenseNetwork.Modules.UIModule.Views.GamePlayView.Scripts
{
    public class PausePanel : HidablePanel
    {
        [Space][Header("Event Channel")] 
        [SerializeField] private VoidEventChannelSO pauseGameEventChannel;
        [SerializeField] private VoidEventChannelSO resumeGameEventChannel;
        [SerializeField] private BoolEventChannelSO musicChannel;
        [SerializeField] private BoolEventChannelSO sfxChannel;
        [SerializeField] private VoidEventChannelSO exitToMenuEventChannel;
        [SerializeField] private VoidEventChannelSO restartLevelEventChannel;
        
        [Space] [Header("Components")] 
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button retryButton;
        [SerializeField] private Button exitButton;
        [SerializeField] private Toggle musicToggle;
        [SerializeField] private Toggle sfxToggle;

        private void OnEnable()
        {
            Initialize();
            pauseGameEventChannel.OnEventRaised += Show;
        }

        private void OnDisable()
        {
            pauseGameEventChannel.OnEventRaised -= Show;
        }

        private void Initialize()
        {
            Hide();
            resumeButton.onClick.AddListener(ResumeGame);
            exitButton.onClick.AddListener(()=> exitToMenuEventChannel.RaiseEvent());
            retryButton.onClick.AddListener(() =>restartLevelEventChannel.RaiseEvent());
            musicToggle.onValueChanged.AddListener(HandleToggleMusic);
            sfxToggle.onValueChanged.AddListener(HandleSFXToggle);
        }
        
        private void HandleSFXToggle(bool value) => sfxChannel.RaiseEvent(value);
        private void HandleToggleMusic(bool value) => musicChannel.RaiseEvent(value);
        

        private void PauseGame()
        {
            pauseGameEventChannel.RaiseEvent();
            Show();
        }

        private void ResumeGame()
        {
            resumeGameEventChannel.RaiseEvent();
            Hide();
        }
    }
}