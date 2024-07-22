using DefenseNetwork.CoreTowerDefense.Requests;
using GameSystemsCookbook;
using UnityEngine;

namespace DefenseNetwork.CoreTowerDefense.ScriptableObjects
{
    /// <summary>
    /// This event channel broadcasts and carries PathRequest payload.
    /// </summary>
    [CreateAssetMenu(fileName = "PathRequestEventChannelSO", menuName = "Events/PathRequestEventChannelSO")]
    public class PathRequestEventChannelSO : GenericEventChannelSO<PathRequest>
    {
        
    }
}