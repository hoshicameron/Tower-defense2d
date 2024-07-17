using System;
using System.Collections.Generic;
using DefenseNetwork.Modules.UIModule.GamePlay.TowerSpotView.Scripts.Enums;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefenseNetwork.Modules.UIModule.GamePlay.TowerSpotView.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "new TowerSpotData", menuName = "Gameplay/Data/TowerSpotData")]
    public class TowerSpotDataSO : ScriptableObject
    {
        [field: SerializeField] public List<TowerData> AvailableTowers { get; private set; }

        [Serializable]
        public class TowerData
        {
            public string Name { get; private set; }
            public int DeployCost { get; private set; }
            public Sprite TowerSprite { get; private set; }
            public TowerType Type { get; private set; }
            public string Description { get; private set; }
        }
    }
}