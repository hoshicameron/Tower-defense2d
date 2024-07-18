using System.Collections.Generic;

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

    }
}