using DefenseNetwork.CoreTowerDefense.DataTransferObjects;
using GameSystemsCookbook;
using UnityEngine;

namespace DefenseNetwork.CoreTowerDefense.ScriptableObjects
{
    /// <summary>
    /// This event channel broadcasts and carries HitDTO payload.
    /// </summary>
    [CreateAssetMenu(fileName = "HitEventChannelSO", menuName = "Events/HitEventChannelSO")]
    public class HitEventChannelSO : GenericEventChannelSO<HitDTO>
    {
        
    }
}