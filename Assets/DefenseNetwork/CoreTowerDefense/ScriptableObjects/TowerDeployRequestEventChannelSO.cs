using DefenseNetwork.CoreTowerDefense.DataRequestObjects;
using GameSystemsCookbook;
using UnityEngine;

namespace DefenseNetwork.CoreTowerDefense.ScriptableObjects
{
    /// <summary>
    /// This event channel broadcasts and carries TowerDeployRequest payload.
    /// </summary>
    [CreateAssetMenu(fileName = "TowerDeployRequestEventChannelSO", menuName = "Events/TowerDeployRequestEventChannelSO")]
    public class TowerDeployRequestEventChannelSO : GenericEventChannelSO<TowerDeployRequest>
    {
        
    }
}