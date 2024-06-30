using System;
using System.Collections.Generic;
using DefenseNetwork.Modules.UIModule.GamePlay.TowerSpotView.Scripts.Enums;
using UnityEngine;

namespace DefenseNetwork.Modules.UIModule.GamePlay.TowerSpotView.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "new TowerSpotData", menuName = "Gameplay/Data/TowerSpotData")]
    public class TowerSpotDataSO : ScriptableObject
    {
        [field: SerializeField] public List<TowerData> AvailableTowers { get; private set; }

        [Serializable]
        public class TowerData
        {
            public string name;
            public int DeployCost;
            public Sprite TowerSprite;
            public TowerType Type;
        }
    }
}