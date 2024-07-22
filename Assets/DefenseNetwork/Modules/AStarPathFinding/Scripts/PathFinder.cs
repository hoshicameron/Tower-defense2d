using System.Collections.Generic;
using System.Linq;
using DefenseNetwork.Core.EventChannels.DataObjects;
using DefenseNetwork.CoreTowerDefense.Requests;
using DefenseNetwork.CoreTowerDefense.ScriptableObjects;
using GameSystemsCookbook;
using UnityEngine;

namespace DefenseNetwork.Modules.AStarPathFinding.Scripts
{
    public class PathFinder : MonoBehaviour
    {
        [Header("Channel")] 
        [SerializeField] private PathRequestEventChannelSO requestPathEventChannel;
        

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

        private void PathRequested(PathRequest pathRequest)
        {
            var cacheKey = (pathRequest.StartPosition, pathRequest.EndPosition);
            
            if (pathCache.TryGetValue(cacheKey, out var cachedPath))
            {
                pathRequest.InvokeResult(cachedPath);
                return;
            }
            
            pathBuilder.SetStartPosition(pathRequest.StartPosition);
            pathBuilder.SetEndPosition(pathRequest.EndPosition);
            var path = pathBuilder.CreatePath();
            if (path == null)
            {
                Debug.LogError("There is no path between Start and End position!!!");
                return;
            }

            var pathList = path.ToList();
            pathCache[cacheKey] = pathList;
            
            pathRequest.InvokeResult(pathList);
        }
       
    }
}