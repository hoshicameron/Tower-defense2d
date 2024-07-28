using UnityEngine;

namespace GameSystemsCookbook
{
    /// <summary>
    /// This event channel broadcasts and carries AudioSource payload.
    /// </summary>
    [CreateAssetMenu(fileName = "AudioClipEventChannelSO", menuName = "Events/AudioClipEventChannelSO")]
    public class AudioEventChannelSO : GenericEventChannelSO<AudioClip>
    {
        
    }
}