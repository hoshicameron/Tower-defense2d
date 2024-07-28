using System.Collections;
using GameSystemsCookbook;
using UnityEngine;

namespace DefenseNetwork.Modules.UIModule.Views.SplashScreen.Scripts
{
    public class SplashScreenView : MonoBehaviour
    {
        [Header("Event Channel")]
        [SerializeField] private VoidEventChannelSO showMainMenuEventChannel;
        
        [SerializeField] private float splashDuration = 3f;
        
        private void Start()
        {
            StartCoroutine(ShowSplashAndLoadMainMenu());
        }

        private IEnumerator ShowSplashAndLoadMainMenu()
        {
            yield return new WaitForSeconds(splashDuration);

            showMainMenuEventChannel.RaiseEvent();
        }
    }
}