using UnityEngine;

namespace DefenseNetwork.MainSystem.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New PlayerDataSO", menuName = "Gameplay/Data/Player Data")]
    public class PlayerDataSO : ScriptableObject
    {
        [field:SerializeField] public int MaxHealth { get; private set; }
    }
}