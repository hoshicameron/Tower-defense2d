using DefenseNetwork.Core.EventChannels.DataObjects;
using DefenseNetwork.CoreTowerDefense.DataRequestObjects;
using UnityEngine;

namespace GameSystemsCookbook
{
    /// <summary>
    /// This event channel broadcasts and carries EnemySpawnRequestDTO payload.
    /// </summary>
    [CreateAssetMenu(fileName = "EnemySpawnRequestChannelSO", menuName = "Events/EnemySpawnRequestChannelSO")]
    public class EnemySpawnRequestChannelSO : GenericEventChannelSO<EnemySpawnRequest>
    {
        
    }
}