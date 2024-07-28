using System;
using GameSystemsCookbook;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace DefenseNetwork.Modules.UIModule.Views.MainMenuView.Scripts
{
    public class MainMenuView : MonoBehaviour
    {
        [Header("Event Channel")] 
        [SerializeField] private VoidEventChannelSO showGameplayEventChannel;
        
        [Space][Header("Buttons")]
        [SerializeField] private Button startGameButton;
        [SerializeField] private Button continueButton;
        [SerializeField] private Button optionsButton;
        [SerializeField] private Button creditsButton;
        [SerializeField] private Button exitButton;

        private void Start()
        {
            startGameButton.onClick.AddListener(() => showGameplayEventChannel.RaiseEvent());
            continueButton.onClick.AddListener(ContinueGame);
            optionsButton.onClick.AddListener(ShowOptions);
            creditsButton.onClick.AddListener(showCredits);
            exitButton.onClick.AddListener(ExitGame);
        }

        private void ExitGame()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        private void showCredits()
        {
            
        }

        private void ShowOptions()
        {
            
        }

        private void ContinueGame()
        {
            
        }
    }
}