using System;
using System.Collections.Generic;
using DefenseNetwork.Core.EventChannels.DataObjects;
using DefenseNetwork.CoreTowerDefense.Requests;
using GameSystemsCookbook;
using UnityEngine;

namespace DefenseNetwork.Modules.EnemyModule.Scripts
{
    public class PathRequester : MonoBehaviour
    {
        [Header("EventChannel")]
        [SerializeField] private PathRequestEventChannelSO requestPathEventChannel;

        [Space]
        [Header("Positions")]
        [SerializeField] private Transform startPoint;
        [SerializeField] private Transform endPoint;

        public List<Vector3> CurrentPath { get; private set; }
        public event Action OnPathUpdated;

        private void Start()
        {
            RequestPath();
        }

        private void RequestPath()
        {
            var pathRequest = new PathRequest
            {
                StartPosition = startPoint.position,
                EndPosition = endPoint.position
            };
            pathRequest.OnRequestResult += HandlePathResult; 
            
            requestPathEventChannel.RaiseEvent(pathRequest);
        }

        private void HandlePathResult(List<Vector3> pathPoints)
        {
            CurrentPath = pathPoints;
            OnPathUpdated?.Invoke();
        }
    }
}