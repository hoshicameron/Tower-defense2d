using DefenseNetwork.Core.EventChannels.DataObjects;
using DefenseNetwork.CoreTowerDefense.DataRequestObjects;
using UnityEngine;

namespace GameSystemsCookbook
{
    /// <summary>
    /// This event channel broadcasts and carries TowerDeployRequest payload.
    /// </summary>
    [CreateAssetMenu(fileName = "TowerDeployRequestEventChannelSO", menuName = "Events/TowerDeployRequestEventChannelSO")]
    public class TowerDeployRequestEventChannelSO : GenericEventChannelSO<TowerDeployRequest>
    {
        
    }
}