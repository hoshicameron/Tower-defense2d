using UnityEngine;

namespace DefenseNetwork.Modules.UIModule.Views.GamePlayView.Scripts
{
    public abstract class HidablePanel : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        
        protected void Hide()
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

        protected void Show()
        {
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
    }
}