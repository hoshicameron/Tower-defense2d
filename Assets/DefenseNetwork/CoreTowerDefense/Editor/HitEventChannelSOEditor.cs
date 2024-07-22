using DefenseNetwork.CoreTowerDefense.DataTransferObjects;
using DefenseNetwork.CoreTowerDefense.ScriptableObjects;
using GameSystemsCookbook;
using UnityEditor;

namespace DefenseNetwork.CoreTowerDefense.Editor
{
    [CustomEditor(typeof(HitEventChannelSO))]
    public class HitEventChannelSOEditor : GenericEventChannelSOEditor<HitDTO>
    {
        
    }
}