using System.Collections.Generic;
using UnityEngine;

namespace GameSystemsCookbook
{
    /// <summary>
    /// This is an example Runtime Set used for tracking one or more GameObjects or components at runtime.
    /// This can replace using singleton instances (which tend to make testing more difficult).
	///
	/// Note: reference the RuntimeSetSO for a Generic base class.
	/// 
    /// </summary>
    [CreateAssetMenu(menuName = "GameSystems/GameObject Runtime Set", fileName = "GameObjectRuntimeSet")]
    public class GameObjectRuntimeSetSO : DescriptionSO
    {
        // Event for the Editor script 
        public System.Action ItemsChanged;
        
        // New event to notify when the set becomes empty
        public System.Action OnSetBecameEmpty;

        [Header("Optional")]
        [Tooltip("Notify other objects this Runtime Set has changed")]
        [SerializeField, Optional] private VoidEventChannelSO m_RuntimeSetUpdated;
        
        [Tooltip("Notify other objects when this Runtime Set becomes empty")]
        [SerializeField, Optional] private VoidEventChannelSO m_RuntimeSetBecameEmpty;

        // Use the Items to track a list of GameObjects at runtime.
        public List<GameObject> Items { get; set; } = new();

        private void OnEnable()
        {
            ItemsChanged?.Invoke();
        }

        // Adds one GameObject to the Items
        public void Add(GameObject thingToAdd)
        {
            if (!Items.Contains(thingToAdd))
                Items.Add(thingToAdd);

            if (m_RuntimeSetUpdated != null)
                m_RuntimeSetUpdated.RaiseEvent();

            ItemsChanged?.Invoke();
        }

        // Removes one GameObject from the Items
        public void Remove(GameObject thingToRemove)
        {
            if (Items.Contains(thingToRemove))
                Items.Remove(thingToRemove);

            if (m_RuntimeSetUpdated != null)
                m_RuntimeSetUpdated.RaiseEvent();
            

            ItemsChanged?.Invoke();
            
            if (Items.Count == 0)
            {
                OnSetBecameEmpty?.Invoke();
                if (m_RuntimeSetBecameEmpty != null)
                    m_RuntimeSetBecameEmpty.RaiseEvent();
            }

        }

        // Reset the list
        public void Clear()
        {
            bool wasNotEmpty = Items.Count > 0;
            
            Items.Clear();

            if (m_RuntimeSetUpdated != null)
                m_RuntimeSetUpdated.RaiseEvent();

            ItemsChanged?.Invoke();
            
            // If the set wasn't empty before clearing, raise the empty event
            if (wasNotEmpty)
            {
                OnSetBecameEmpty?.Invoke();
                if (m_RuntimeSetBecameEmpty != null)
                    m_RuntimeSetBecameEmpty.RaiseEvent();
            }
        }

        // Clean up any items after the list is cleared
        public void DestroyItems()
        {
            foreach (var item in Items)
            {
                Destroy(item);
            }
        }
    }
}