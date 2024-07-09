using DefenseNetwork.Core.EventChannels.DataObjects;
using UnityEditor;

namespace GameSystemsCookbook
{
    [CustomEditor(typeof(EnemySpawnRequestChannelSO))]
    public class EnemySpawnRequestChannelSOEditor : GenericEventChannelSOEditor<EnemySpawnRequestDTO>
    {
        
    }
}