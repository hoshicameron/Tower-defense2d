using DefenseNetwork.CoreTowerDefense.DataRequestObjects;
using DefenseNetwork.CoreTowerDefense.ScriptableObjects;
using GameSystemsCookbook;
using UnityEditor;

namespace DefenseNetwork.CoreTowerDefense.Editor
{
    [CustomEditor(typeof(TowerModificationRequestEventChannelSO))]
    public class TowerModificationRequestEventChannelSOEditor : GenericEventChannelSOEditor<TowerModificationRequest>
    {
        
    }
}