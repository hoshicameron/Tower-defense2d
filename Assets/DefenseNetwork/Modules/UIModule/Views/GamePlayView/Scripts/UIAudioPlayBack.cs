using GameSystemsCookbook;
using UnityEngine;

namespace DefenseNetwork.Modules.UIModule.Views.GamePlayView.Scripts
{
    public class UIAudioPlayBack : MonoBehaviour
    {
        [Header("Event Channel")] 
        [SerializeField] private AudioEventChannelSO soundEffectChannel;

        [Space] [Header("Audio clip to play")] 
        [SerializeField] private AudioClip clipToPlay;

        public void PlayAudio() => soundEffectChannel.RaiseEvent(clipToPlay);
    }
}