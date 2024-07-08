using System;
using System.Linq;
using DefenseNetwork.Core.EventChannels.DataObjects;
using GameSystemsCookbook;
using UnityEngine;

namespace DefenseNetwork.Modules.AStarPathFinding.Scripts
{
    public class PathFinder : MonoBehaviour
    {
        [Header("Channel")] 
        [SerializeField] private PathRequestEventChannelSO requestPathEventChannel;
        [SerializeField] private PathResponseEventChannelSO responsePathEventChannel;

        [Space] [Header("Path")] 
        [SerializeField] private Map map;

        private AStarPathBuilder pathBuilder;
        private void OnEnable()
        {
            map.SetupPathTilemap();
            pathBuilder = new AStarPathBuilder(map);
            requestPathEventChannel.OnEventRaised += PathRequested;
        }

        private void OnDisable()
        {
            requestPathEventChannel.OnEventRaised -= PathRequested;
        }

        private void PathRequested(PathRequestDTO pathRequestDto)
        {
            pathBuilder.SetStartPosition(pathRequestDto.StartPosition);
            pathBuilder.SetEndPosition(pathRequestDto.EndPosition);
            var path = pathBuilder.CreatePath();
            pathBuilder.VisualizePath();
            if (path == null)
            {
                Debug.LogError("There is no path between Start and End position!!!");
                return;
            }
            
            responsePathEventChannel.RaiseEvent(
                new PathResponseDTO
                {
                    RequestID = pathRequestDto.RequestID,
                    PathPoints = path.ToList()
                });
        }

        private void Start()
        {
            
        }
    }
}