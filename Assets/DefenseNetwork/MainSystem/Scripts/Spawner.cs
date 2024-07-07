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

        private void PathCreated(PathResponseDO pathResponseDo)
        {
            if(string.Compare(requestID, pathResponseDo.RequestID, StringComparison.Ordinal) != 0)
                return;
            
            movementPath = pathResponseDo.PathPoints;
        }

        private void Start()
        {
            requestID = Guid.NewGuid().ToString();
            requestPathEventChannel.RaiseEvent(new PathRequestDO
            {
                RequestID = requestID,
                StartPosition = startPoint.position,
                EndPosition = endPoint.position
            });
        }
    }
}