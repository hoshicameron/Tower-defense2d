using System.Collections.Generic;
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

        private Dictionary<(Vector3, Vector3), List<Vector3>> pathCache = new();
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
            var cacheKey = (pathRequestDto.StartPosition, pathRequestDto.EndPosition);
            
            if (pathCache.TryGetValue(cacheKey, out var cachedPath))
            {
                SendPathResponse(pathRequestDto.RequestID, cachedPath);
                return;
            }
            
            pathBuilder.SetStartPosition(pathRequestDto.StartPosition);
            pathBuilder.SetEndPosition(pathRequestDto.EndPosition);
            var path = pathBuilder.CreatePath();
            pathBuilder.VisualizePath();
            
            if (path == null)
            {
                Debug.LogError("There is no path between Start and End position!!!");
                return;
            }

            var pathList = path.ToList();
            pathCache[cacheKey] = pathList;
            SendPathResponse(pathRequestDto.RequestID, pathList);
        }
        private void SendPathResponse(string requestID, List<Vector3> path)
        {
            responsePathEventChannel.RaiseEvent(
                new PathResponseDTO
                {
                    RequestID = requestID,
                    PathPoints = path
                });
        }
    }
}