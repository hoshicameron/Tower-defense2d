using UnityEngine;

namespace DefenseNetwork.Modules.EnemyModule.Scripts.ScriptableObjects.Data
{
    [CreateAssetMenu(fileName = "New EnemyData", menuName = "Gameplay/Data/Enemy Data", order = 0)]
    public class EnemyDataSO : ScriptableObject
    {
        [field:SerializeField] public float Speed { get; private set; }
        [field:SerializeField] public float RotationSpeed { get; private set; }
        [field:SerializeField] public int Health { get; private set; }
        [field:SerializeField] public Sprite Sprite { get; private set; }
        [field:SerializeField] public string LayerName { get; private set; }
        
        public int EnemyLayer => LayerMask.NameToLayer(LayerName);
    }
}