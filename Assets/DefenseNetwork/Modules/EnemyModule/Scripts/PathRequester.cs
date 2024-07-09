using System;
using System.Collections.Generic;
using DefenseNetwork.Core.EventChannels.DataObjects;
using GameSystemsCookbook;
using UnityEngine;

namespace DefenseNetwork.Modules.EnemyModule.Scripts
{
    public class PathRequester : MonoBehaviour
    {
        [Header("EventChannel")]
        [SerializeField] private PathRequestEventChannelSO requestPathEventChannel;
        [SerializeField] private PathResponseEventChannelSO responsePathEventChannel;
        
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
            var requestID = Guid.NewGuid().ToString();
            requestPathEventChannel.RaiseEvent(new PathRequestDTO
            {
                RequestID = requestID,
                StartPosition = startPoint.position,
                EndPosition = endPoint.position
            });
        }

        private void OnEnable()
        {
            responsePathEventChannel.OnEventRaised += OnPathReceived;
        }

        private void OnDisable()
        {
            responsePathEventChannel.OnEventRaised -= OnPathReceived;
        }

        private void OnPathReceived(PathResponseDTO response)
        {
            CurrentPath = response.PathPoints;
            OnPathUpdated?.Invoke();
        }
    }
}