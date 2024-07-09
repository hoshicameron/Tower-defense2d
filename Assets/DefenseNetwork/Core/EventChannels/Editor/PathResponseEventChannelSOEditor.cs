using DefenseNetwork.Core.EventChannels.DataObjects;
using UnityEditor;

namespace GameSystemsCookbook
{
    [CustomEditor(typeof(PathResponseEventChannelSO))]
    public class PathResponseEventChannelSOEditor : GenericEventChannelSOEditor<PathResponseDTO>
    {
        
    }
}