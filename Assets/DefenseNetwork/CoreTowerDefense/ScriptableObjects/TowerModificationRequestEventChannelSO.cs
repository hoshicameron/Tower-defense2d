using DefenseNetwork.CoreTowerDefense.DataRequestObjects;
using GameSystemsCookbook;
using UnityEngine;

namespace DefenseNetwork.CoreTowerDefense.ScriptableObjects
{
    /// <summary>
    /// This event channel broadcasts and carries TowerModificationRequest payload.
    /// </summary>
    [CreateAssetMenu(fileName = "TowerModificationRequestEventChannelSO", menuName = "Events/TowerModificationRequestEventChannelSO")]
    public class TowerModificationRequestEventChannelSO : GenericEventChannelSO<TowerModificationRequest>
    {
        
    }
}