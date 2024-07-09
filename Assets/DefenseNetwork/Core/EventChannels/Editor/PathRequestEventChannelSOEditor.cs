using DefenseNetwork.Core.EventChannels.DataObjects;
using UnityEditor;

namespace GameSystemsCookbook
{
    [CustomEditor(typeof(PathRequestEventChannelSO))]
    public class PathRequestEventChannelSOEditor : GenericEventChannelSOEditor<PathRequestDTO>
    {
        
    }
}