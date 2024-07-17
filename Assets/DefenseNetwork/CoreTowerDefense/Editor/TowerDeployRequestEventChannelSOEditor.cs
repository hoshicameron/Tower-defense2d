using DefenseNetwork.Core.EventChannels.DataObjects;
using DefenseNetwork.CoreTowerDefense.DataRequestObjects;
using UnityEditor;

namespace GameSystemsCookbook
{
    [CustomEditor(typeof(TowerDeployRequestEventChannelSO))]
    public class TowerDeployRequestEventChannelSOEditor : GenericEventChannelSOEditor<TowerDeployRequest>
    {
      
    }
}