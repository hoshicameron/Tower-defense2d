using DefenseNetwork.Core.EventChannels.DataObjects;
using UnityEditor;

namespace GameSystemsCookbook
{
    [CustomEditor(typeof(TowerDeployRequestEventChannelSO))]
    public class TowerDeployRequestEventChannelSOEditor : GenericEventChannelSOEditor<TowerDeployRequestDTO>
    {
      
    }
}