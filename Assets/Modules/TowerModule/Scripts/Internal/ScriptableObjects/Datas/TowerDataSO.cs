using UnityEngine;

namespace Modules.TowerModule.Scripts.Internal.ScriptableObjects.Datas
{
    [CreateAssetMenu(fileName = "New TowerData", menuName = "Gameplay/Datas/TowerData")]
    public class TowerDataSO : ScriptableObject
    {
         
         [field: SerializeField] public float Range { get; private set; } = 5;
         [field:SerializeField] public Sprite BaseSprite { get; private set; }
         [field:SerializeField]public LayerMask DetectionLayer { get; private set; }
    }
}