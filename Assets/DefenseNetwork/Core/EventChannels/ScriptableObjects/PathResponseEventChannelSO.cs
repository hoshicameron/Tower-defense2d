using DefenseNetwork.Core.EventChannels.DataObjects;
using UnityEngine;

namespace GameSystemsCookbook
{
    /// <summary>
    /// This event channel broadcasts and carries PathResponseDO payload.
    /// </summary>
    [CreateAssetMenu(fileName = "PathResponseEventChannelSO", menuName = "Events/PathResponseEventChannelSO")]
    public class PathResponseEventChannelSO : GenericEventChannelSO<PathResponseDO>
    {
        
    }
}