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

        [Header("Optional")]
        [Tooltip("Notify other objects this Runtime Set has changed")]
        [SerializeField, Optional] private VoidEventChannelSO m_RuntimeSetUpdated;

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

        }

        // Reset the list
        public void Clear()
        {
            Items.Clear();

            if (m_RuntimeSetUpdated != null)
                m_RuntimeSetUpdated.RaiseEvent();

            ItemsChanged?.Invoke();
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