using DefenseNetwork.Core.EventChannels.DataObjects;
using DefenseNetwork.CoreTowerDefense.DataTransferObjects;
using UnityEditor;

namespace GameSystemsCookbook
{
    [CustomEditor(typeof(HitEventChannelSO))]
    public class HitEventChannelSOEditor : GenericEventChannelSOEditor<HitDTO>
    {
        
    }
}