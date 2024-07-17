using DefenseNetwork.Core.EventChannels.DataObjects;
using DefenseNetwork.CoreTowerDefense.DataRequestObjects;
using UnityEditor;

namespace GameSystemsCookbook
{
    [CustomEditor(typeof(EnemySpawnRequestChannelSO))]
    public class EnemySpawnRequestChannelSOEditor : GenericEventChannelSOEditor<EnemySpawnRequest>
    {
        
    }
}