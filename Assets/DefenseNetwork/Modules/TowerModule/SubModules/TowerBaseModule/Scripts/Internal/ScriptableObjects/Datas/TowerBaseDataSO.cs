using UnityEngine;

namespace DefenseNetwork.Modules.TowerModule.SubModules.TowerBaseModule.Scripts.Internal.ScriptableObjects.Datas
{
    [CreateAssetMenu(fileName = "New TowerBaseData", menuName = "Gameplay/Data/TowerBaseData")]
    public class TowerBaseDataSO : ScriptableObject
    {
         [field:SerializeField] public Sprite BaseSprite { get; private set; }
         [field:SerializeField] public LayerMask DetectionLayer { get; private set; }
    }
}