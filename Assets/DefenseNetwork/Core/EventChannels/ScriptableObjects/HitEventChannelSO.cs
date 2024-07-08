using DefenseNetwork.Core.EventChannels.DataObjects;
using UnityEngine;

namespace GameSystemsCookbook
{
    /// <summary>
    /// This event channel broadcasts and carries HitDTO payload.
    /// </summary>
    [CreateAssetMenu(fileName = "HitEventChannelSO", menuName = "Events/HitEventChannelSO")]
    public class HitEventChannelSO : GenericEventChannelSO<HitDTO>
    {
        
    }
}