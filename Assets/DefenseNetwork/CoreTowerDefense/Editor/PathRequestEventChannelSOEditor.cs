using DefenseNetwork.CoreTowerDefense.Requests;
using DefenseNetwork.CoreTowerDefense.ScriptableObjects;
using GameSystemsCookbook;
using UnityEditor;

namespace DefenseNetwork.CoreTowerDefense.Editor
{
    [CustomEditor(typeof(PathRequestEventChannelSO))]
    public class PathRequestEventChannelSOEditor : GenericEventChannelSOEditor<PathRequest>
    {
        
    }
}