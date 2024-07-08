using System;
using System.Collections.Generic;
using DefenseNetwork.Core.EventChannels.DataObjects;
using GameSystemsCookbook;
using UnityEngine;

namespace DefenseNetwork.MainSystem.Scripts
{
    public class Spawner : MonoBehaviour
    {
        [Header("Channel")] 
        [SerializeField] private PathRequestEventChannelSO requestPathEventChannel;
        [SerializeField] private PathResponseEventChannelSO responsePathEventChannel;

        [Space] 
        [SerializeField] private Transform startPoint;
        [SerializeField] private Transform endPoint;

        private List<Vector3> movementPath;
        
        private string requestID;
        private void OnEnable()
        {
            responsePathEventChannel.OnEventRaised += PathCreated;
        }

        private void OnDisable()
        {
            responsePathEventChannel.OnEventRaised -= PathCreated;
        }

        private void PathCreated(PathResponseDTO pathResponseDto)
        {
            if(string.Compare(requestID, pathResponseDto.RequestID, StringComparison.Ordinal) != 0)
                return;
            
            movementPath = pathResponseDto.PathPoints;
        }

        private void Start()
        {
            requestID = Guid.NewGuid().ToString();
            requestPathEventChannel.RaiseEvent(new PathRequestDTO
            {
                RequestID = requestID,
                StartPosition = startPoint.position,
                EndPosition = endPoint.position
            });
        }
    }
}