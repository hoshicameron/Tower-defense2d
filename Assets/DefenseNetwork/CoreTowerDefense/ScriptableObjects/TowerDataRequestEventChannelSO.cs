using DefenseNetwork.Core.EventChannels.DataObjects;
using DefenseNetwork.CoreTowerDefense.DataRequestObjects;
using DefenseNetwork.CoreTowerDefense.Requests;
using UnityEngine;

namespace GameSystemsCookbook
{
    /// <summary>
    /// This event channel broadcasts and carries Boolean payload.
    /// </summary>
    
    [CreateAssetMenu(fileName = "TowerDataEventChannelSO", menuName = "Events/TowerDataEventChannelSO")]
    public class TowerDataRequestEventChannelSO : GenericEventChannelSO<TowerDataRequest>
    {
        
    }
}