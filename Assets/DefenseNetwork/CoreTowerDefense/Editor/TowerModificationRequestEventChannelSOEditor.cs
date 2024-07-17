using DefenseNetwork.Core.EventChannels.DataObjects;
using DefenseNetwork.CoreTowerDefense.DataRequestObjects;
using UnityEditor;

namespace GameSystemsCookbook
{
    [CustomEditor(typeof(TowerModificationRequestEventChannelSO))]
    public class TowerModificationRequestEventChannelSOEditor : GenericEventChannelSOEditor<TowerModificationRequest>
    {
        
    }
}