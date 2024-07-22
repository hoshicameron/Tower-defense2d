using DefenseNetwork.CoreTowerDefense.Enums;
using GameSystemsCookbook;
using UnityEngine;

namespace DefenseNetwork.CoreTowerDefense.ScriptableObjects
{
    /// <summary>
    /// This event channel broadcasts and carries GameState payload.
    /// </summary>
    [CreateAssetMenu(fileName = "GameStateEventChannelSO", menuName = "Events/GameStateEventChannelSO")]
    public class GameStateEventChannelSO : GenericEventChannelSO<GameState>
    {
        
    }
}