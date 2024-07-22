using DefenseNetwork.CoreTowerDefense.Enums;
using DefenseNetwork.CoreTowerDefense.ScriptableObjects;
using GameSystemsCookbook;
using UnityEditor;

namespace DefenseNetwork.CoreTowerDefense.Editor
{
    [CustomEditor(typeof(GameStateEventChannelSO))]
    public class GameStateEventChannelSOEditor : GenericEventChannelSOEditor<GameState>
    {
        
    }
}