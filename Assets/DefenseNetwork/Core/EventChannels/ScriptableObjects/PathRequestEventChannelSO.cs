using DefenseNetwork.Core.EventChannels.DataObjects;
using UnityEngine;

namespace GameSystemsCookbook
{
    /// <summary>
    /// This event channel broadcasts and carries PathRequestDO payload.
    /// </summary>
    [CreateAssetMenu(fileName = "PathRequestEventChannelSO", menuName = "Events/PathRequestEventChannelSO")]
    public class PathRequestEventChannelSO : GenericEventChannelSO<PathRequestDO>
    {
        
    }
}