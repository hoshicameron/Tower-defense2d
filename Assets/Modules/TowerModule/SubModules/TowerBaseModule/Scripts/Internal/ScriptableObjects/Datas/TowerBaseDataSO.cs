using UnityEngine;

namespace Modules.TowerModule.SubModules.TowerBaseModule.Scripts.Internal.ScriptableObjects.Datas
{
    [CreateAssetMenu(fileName = "New TowerBaseData", menuName = "Gameplay/Datas/TowerBase Data")]
    public class TowerBaseDataSO : ScriptableObject
    {
         [field:SerializeField] public float Range { get; private set; } = 5;
         [field:SerializeField] public Sprite BaseSprite { get; private set; }
         [field:SerializeField] public LayerMask DetectionLayer { get; private set; }
    }
}