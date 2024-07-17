using DefenseNetwork.Core.EventChannels.DataObjects;
using UnityEditor;

namespace GameSystemsCookbook
{
    [CustomEditor(typeof(TowerModificationRequestEventChannelSO))]
    public class TowerModificationRequestEventChannelSOEditor : GenericEventChannelSOEditor<TowerModificationRequestDTO>
    {
        
    }
}