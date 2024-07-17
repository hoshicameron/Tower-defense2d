using DefenseNetwork.Core.EventChannels.DataObjects;
using DefenseNetwork.CoreTowerDefense.DataRequestObjects;
using DefenseNetwork.CoreTowerDefense.Requests;
using UnityEditor;

namespace GameSystemsCookbook
{
    [CustomEditor(typeof(TowerDataRequestEventChannelSO))]
    public class TowerDataRequestEventChannelSOEditor : GenericEventChannelSOEditor<TowerDataRequest>
    {
        
    }
}