﻿using DefenseNetwork.Core.EventChannels.DataObjects.Enums;
using DefenseNetwork.CoreTowerDefense.Enums;
using UnityEngine;

namespace DefenseNetwork.CoreTowerDefense.DataRequestObjects
{
    public struct TowerDeployRequest
    {
        public Vector2 DeployPosition { get; set; }
        public int DeployCost { get; set; }
        public TowerType TowerType { get; set; }
        
        public delegate void RequestResultHandler(RequestResult result, string message);
        
        public event RequestResultHandler OnRequestResult;
        public void InvokeResult(RequestResult result, string message)
        {
            OnRequestResult?.Invoke(result, message);
        }
    }
}