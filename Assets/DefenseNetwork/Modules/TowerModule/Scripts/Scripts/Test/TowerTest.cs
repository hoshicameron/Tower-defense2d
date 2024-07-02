using GameSystemsCookbook;
using UnityEngine;

namespace DefenseNetwork.Modules.TowerModule.Scripts.Scripts.Test
{
    public class TowerTest : MonoBehaviour
    {
        [Header("Channel")] 
        [SerializeField] private IntEventChannelSO modifyPointEventChannel;
        [SerializeField] private IntEventChannelSO updatePointEventChannel;
        
        [Space] [SerializeField] private int point = 200;

        private void OnEnable()
        {
            modifyPointEventChannel.OnEventRaised += UpdatePoint;   
        }

        private void OnDisable()
        {
            modifyPointEventChannel.OnEventRaised -= UpdatePoint;
        }

        private void UpdatePoint(int valueChange)
        {
            point += valueChange;
            updatePointEventChannel.RaiseEvent(point);
        }

        private void Start()
        {
            updatePointEventChannel.RaiseEvent(point);
        }
    }
}
