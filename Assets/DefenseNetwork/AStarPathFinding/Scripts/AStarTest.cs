using UnityEngine;

namespace DefenseNetwork.AStarPathFinding.Scripts
{
    public class AStarTest : MonoBehaviour
    {
        [SerializeField] private Map map;
    
        private PathManager pathManager;
        private Camera mainCamera;

        private void Start()
        {
            mainCamera = Camera.main;
            pathManager = new PathManager(map);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                pathManager.ClearPath();
                pathManager.SetStartPosition(GetMouseWorldPosition());
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                pathManager.ClearPath();
                pathManager.SetEndPosition(GetMouseWorldPosition());
            }

            if (Input.GetKeyDown(KeyCode.U))
            {
                pathManager.VisualizePath();
            }
        }

        private Vector3 GetMouseWorldPosition()
        {
            var mousePosition = Input.mousePosition;
            return mainCamera.ScreenToWorldPoint(mousePosition);
        }
    }
}