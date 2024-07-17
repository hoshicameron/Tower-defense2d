using System;
using System.Collections.Generic;
using DefenseNetwork.Core.EventChannels.DataObjects.Enums;
using UnityEngine;

namespace DefenseNetwork.CoreTowerDefense.Requests
{
    public struct TowerDataRequest
    {
        public delegate void RequestResultHandler(List<ITowerData> availableTowers);
        
        public event RequestResultHandler OnRequestResult;
        public void InvokeResult(List<ITowerData> availableTowers)
        {
            OnRequestResult?.Invoke(availableTowers);
        }
        
        [Serializable]
        public struct TowerData : ITowerData
        {
            public string Name { get; private set; }
            public int DeployCost { get; private set; }
            public int UpgradeCost { get; private set; }
            public GameObject Prefab{ get; private set; }
            public TowerType Type { get; private set; }
            public Sprite Sprite{ get; private set; }
            public string Description { get; private set; }
            
            public int SellIncome => DeployCost - (int)(DeployCost * 0.2f);
        }
    }
}