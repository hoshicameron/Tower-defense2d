using GameSystemsCookbook;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace DefenseNetwork.MainSystem.Scripts
{
    public class LevelManager : MonoBehaviour
    {
        [Header("Event Channel")] 
        [SerializeField] private VoidEventChannelSO showMainMenuEventChannel;
        [SerializeField] private VoidEventChannelSO restartLevelEventChannel;
        [SerializeField] private VoidEventChannelSO continueGameEventChannel;
        [SerializeField] private VoidEventChannelSO showGameplayEventChannel;
        
        [Space]
        [Header("Scenes Name")]
        [SerializeField] private string mainMenuSceneName;
        [SerializeField] private string gameplaySceneName;
        

        private void OnEnable()
        {
            showMainMenuEventChannel.OnEventRaised += ShowMainMenu;
            restartLevelEventChannel.OnEventRaised += RestartLevel;
            continueGameEventChannel.OnEventRaised += ContinueToNextLevel;
            showGameplayEventChannel.OnEventRaised += ShowGameplay;
        }
        
        private void OnDisable()
        {
            showMainMenuEventChannel.OnEventRaised -= ShowMainMenu;
            restartLevelEventChannel.OnEventRaised -= RestartLevel;
            continueGameEventChannel.OnEventRaised -= ContinueToNextLevel;
            showGameplayEventChannel.OnEventRaised -= ShowGameplay;
        }

        private void ShowMainMenu() => LoadScene(mainMenuSceneName);
        private void ShowGameplay() => LoadScene(gameplaySceneName);
        private void RestartLevel() => SceneManager.LoadScene(SceneManager.GetSceneByBuildIndex(SceneManager.GetActiveScene().buildIndex).name);
        private void ContinueToNextLevel() => SceneManager.LoadScene(SceneManager.GetSceneByBuildIndex(SceneManager.GetActiveScene().buildIndex + 1).name);
        
        private void LoadScene(string SceneName)=>SceneManager.LoadScene(SceneName);
    }
}