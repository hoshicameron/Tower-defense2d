using DefenseNetwork.Core.EventChannels.DataObjects;
using DefenseNetwork.CoreTowerDefense.Requests;
using UnityEngine;

namespace GameSystemsCookbook
{
    /// <summary>
    /// This event channel broadcasts and carries PathRequestDO payload.
    /// </summary>
    [CreateAssetMenu(fileName = "PathRequestEventChannelSO", menuName = "Events/PathRequestEventChannelSO")]
    public class PathRequestEventChannelSO : GenericEventChannelSO<PathRequest>
    {
        
    }
}