using DefenseNetwork.Core.EventChannels.DataObjects;
using DefenseNetwork.CoreTowerDefense.Requests;
using UnityEditor;

namespace GameSystemsCookbook
{
    [CustomEditor(typeof(PathRequestEventChannelSO))]
    public class PathRequestEventChannelSOEditor : GenericEventChannelSOEditor<PathRequest>
    {
        
    }
}