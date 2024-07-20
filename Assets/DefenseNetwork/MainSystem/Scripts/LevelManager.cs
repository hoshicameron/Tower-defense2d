using GameSystemsCookbook;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefenseNetwork.MainSystem.Scripts
{
    public class LevelManager : MonoBehaviour
    {
        [Header("Event Channel")] 
        [SerializeField] private VoidEventChannelSO exitToMenuEventChannel;
        [SerializeField] private VoidEventChannelSO restartLevelEventChannel;
        [SerializeField] private VoidEventChannelSO continueGameEventChannel;

        private void OnEnable()
        {
            exitToMenuEventChannel.OnEventRaised += ExitToMenu;
            restartLevelEventChannel.OnEventRaised += RestartLevel;
            continueGameEventChannel.OnEventRaised += ContinueToNextLevel;
        }
        
        private void OnDisable()
        {
            exitToMenuEventChannel.OnEventRaised -= ExitToMenu;
            restartLevelEventChannel.OnEventRaised -= RestartLevel;
            continueGameEventChannel.OnEventRaised -= ContinueToNextLevel;
        }
        
        private void ExitToMenu() => SceneManager.LoadScene(SceneManager.GetSceneByBuildIndex(0).name);
        private void RestartLevel() => SceneManager.LoadScene(SceneManager.GetSceneByBuildIndex(SceneManager.GetActiveScene().buildIndex).name);
        private void ContinueToNextLevel() => SceneManager.LoadScene(SceneManager.GetSceneByBuildIndex(SceneManager.GetActiveScene().buildIndex + 1).name);
    }
}