using DefenseNetwork.Modules.CommonBehavioursModule.Scripts.Interfaces;
using GameSystemsCookbook;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DefenseNetwork.Modules.CommonBehavioursModule.Scripts
{
    public class Selector : MonoBehaviour
    {
        [Header("Event Channel")] 
        [SerializeField] private GameObjectEventChannelSO selectionChannel;
        
        [Space]
        [SerializeField] private LayerMask selectableLayer;
        private Camera mainCamera;

        private void Awake()
        {
            mainCamera = Camera.main;
        }

        private void Update()
        {
            if (!Input.GetMouseButtonUp(0) || EventSystem.current.IsPointerOverGameObject()) return;
    
            var mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            var hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, selectableLayer);

            if (hit.collider != null)
            {
                var selectable = hit.collider.GetComponent<ISelectable>();
                selectable?.Select();
            }
            else
            {
                selectionChannel.RaiseEvent(null);
            }
        }
    }
}