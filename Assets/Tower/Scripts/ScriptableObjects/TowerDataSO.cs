using UnityEngine;

namespace Tower.Scripts
{
    [CreateAssetMenu(fileName = "New TowerData", menuName = "Gameplay/Datas/TowerData")]
    public class TowerDataSO : ScriptableObject
    {
         [field:SerializeField]public float TurnSpeed { get; private set; } = 10.0f;
         [field: SerializeField] public float Range { get; private set; } = 5;
         [field: SerializeField] public float FireRate { get; private set; } = 5;
         [field:SerializeField] public Sprite HeadSprite { get; private set; }
         [field:SerializeField] public Sprite BaseSprite { get; private set; }
         
    }
}