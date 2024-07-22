using DefenseNetwork.CoreTowerDefense.Requests;
using GameSystemsCookbook;
using UnityEngine;

namespace DefenseNetwork.CoreTowerDefense.ScriptableObjects
{
    /// <summary>
    /// This event channel broadcasts and carries TowerDataRequest payload.
    /// </summary>
    
    [CreateAssetMenu(fileName = "TowerDataEventChannelSO", menuName = "Events/TowerDataEventChannelSO")]
    public class TowerDataRequestEventChannelSO : GenericEventChannelSO<TowerDataRequest>
    {
        
    }
}