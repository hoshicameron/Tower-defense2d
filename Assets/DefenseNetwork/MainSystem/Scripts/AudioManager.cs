using GameSystemsCookbook;
using UnityEngine;

namespace DefenseNetwork.MainSystem.Scripts
{
    public class AudioManager : MonoBehaviour
    {
        [Header("Event Channel")] 
        [SerializeField] private AudioEventChannelSO effectChannel;
        [SerializeField] private AudioEventChannelSO musicChannel;
        [SerializeField] private BoolEventChannelSO muteMusicChannel;
        [SerializeField] private BoolEventChannelSO muteEffectsChannel;
        
        [Space][Header("Components")]
        [SerializeField] private AudioSource musicAudioSource;
        [SerializeField] private AudioSource effectAudioSource;
        
        private bool isEffectMuted;
        private bool isMusicMuted;

        private void OnEnable()
        {
            effectChannel.OnEventRaised += PlaySoundEffect;
            musicChannel.OnEventRaised += PlayMusic;
            muteMusicChannel.OnEventRaised += HandleMusicMuteUnmute;
            muteEffectsChannel.OnEventRaised += HandleEffectsMuteUnmute;
        }
        private void OnDisable()
        {
            effectChannel.OnEventRaised -= PlaySoundEffect;
            muteMusicChannel.OnEventRaised -= HandleMusicMuteUnmute;
            muteEffectsChannel.OnEventRaised -= HandleEffectsMuteUnmute;
        }
        
        
        private void PlayMusic(AudioClip clip)
        {
            if(isMusicMuted)    return;
            musicAudioSource.clip = clip;
            musicAudioSource.Play();
        }

        private void HandleEffectsMuteUnmute(bool value)
        {
            effectAudioSource.mute = value;
            isEffectMuted = value;
        }

        private void HandleMusicMuteUnmute(bool value)
        {
            musicAudioSource.mute = value;
            isMusicMuted = value;
        }


        private void PlaySoundEffect(AudioClip clip)
        {
            if(isEffectMuted)  return;
            
            effectAudioSource.PlayOneShot(clip);
        }
    }
}