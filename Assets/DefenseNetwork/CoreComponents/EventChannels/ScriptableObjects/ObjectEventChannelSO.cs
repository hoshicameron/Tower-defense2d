using UnityEngine;

namespace GameSystemsCookbook
{
    /// <summary>
    /// This event channel broadcasts and carries object payload.
    /// </summary>
    [CreateAssetMenu(fileName = "ObjectEventChannelSO", menuName = "Events/ObjectEventChannelSO")]
    public class ObjectEventChannelSO : GenericEventChannelSO<object>
    {
        
    }
}