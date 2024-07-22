using DefenseNetwork.CoreTowerDefense.Requests;
using DefenseNetwork.CoreTowerDefense.ScriptableObjects;
using GameSystemsCookbook;
using UnityEditor;

namespace DefenseNetwork.CoreTowerDefense.Editor
{
    [CustomEditor(typeof(TowerDataRequestEventChannelSO))]
    public class TowerDataRequestEventChannelSOEditor : GenericEventChannelSOEditor<TowerDataRequest>
    {
        
    }
}