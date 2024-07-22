using DefenseNetwork.CoreTowerDefense.DataRequestObjects;
using GameSystemsCookbook;
using UnityEngine;

namespace DefenseNetwork.CoreTowerDefense.ScriptableObjects
{
    /// <summary>
    /// This event channel broadcasts and carries EnemySpawnRequest payload.
    /// </summary>
    [CreateAssetMenu(fileName = "EnemySpawnRequestChannelSO", menuName = "Events/EnemySpawnRequestChannelSO")]
    public class EnemySpawnRequestChannelSO : GenericEventChannelSO<EnemySpawnRequest>
    {
        
    }
}