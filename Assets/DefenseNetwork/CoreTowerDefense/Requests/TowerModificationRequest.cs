using DefenseNetwork.Core.EventChannels.DataObjects.Enums;
using DefenseNetwork.CoreTowerDefense.Enums;

namespace DefenseNetwork.CoreTowerDefense.DataRequestObjects
{
    public struct TowerModificationRequest
    {
        public TowerOperation Operation { get; set; }
        public int Cost { get; set; }
        
        public delegate void RequestResultHandler(RequestResult result, string message);
        
        public event TowerDeployRequest.RequestResultHandler OnRequestResult;
        public void InvokeResult(RequestResult result, string message)
        {
            OnRequestResult?.Invoke(result, message);
        }
    }
}