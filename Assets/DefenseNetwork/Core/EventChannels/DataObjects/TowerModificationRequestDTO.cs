using DefenseNetwork.Core.EventChannels.DataObjects.Enums;

namespace DefenseNetwork.Core.EventChannels.DataObjects
{
    public struct TowerModificationRequestDTO
    {
        public TowerOperation Operation { get; set; }
        public int Cost { get; set; }
        
        public delegate void RequestResultHandler(RequestResult result, string message);
        
        public event TowerDeployRequestDTO.RequestResultHandler OnRequestResult;
        public void InvokeResult(RequestResult result, string message)
        {
            OnRequestResult?.Invoke(result, message);
        }
    }
}