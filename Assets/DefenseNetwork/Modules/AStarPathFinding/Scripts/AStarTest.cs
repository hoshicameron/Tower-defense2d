using DefenseNetwork.Modules.AStarPathFinding.Scripts;
using UnityEngine;

namespace DefenseNetwork.AStarPathFinding.Scripts
{
    public class AStarTest : MonoBehaviour
    {
        [SerializeField] private Map map;
    
        private AStarPathBuilder aStarPathBuilder;
        private Camera mainCamera;
        private void Start()
        {
            mainCamera = Camera.main;
            map.SetupPathTilemap();
            aStarPathBuilder = new AStarPathBuilder(map);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                aStarPathBuilder.ClearPath();
                aStarPathBuilder.SetStartPosition(GetMouseWorldPosition());
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                aStarPathBuilder.ClearPath();
                aStarPathBuilder.SetEndPosition(GetMouseWorldPosition());
            }

            if (Input.GetKeyDown(KeyCode.U))
            {
                aStarPathBuilder.VisualizePath();
            }
        }

        private Vector3 GetMouseWorldPosition()
        {
            var mousePosition = Input.mousePosition;
            return mainCamera.ScreenToWorldPoint(mousePosition);
        }
    }
}