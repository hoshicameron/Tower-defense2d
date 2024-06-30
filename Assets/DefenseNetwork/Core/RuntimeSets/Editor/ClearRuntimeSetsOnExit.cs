using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

namespace GameSystemsCookbook
{
    /// <summary>
    /// This class locates all of the GameObjectRuntimeSetSO objects in the project and clears
    // their lists when exiting play mode.
    /// </summary>

    [InitializeOnLoad]
    public static class ClearRuntimeSetsOnExit
    {
        // Constructor
        static ClearRuntimeSetsOnExit()
        {
            // Register the OnPlayModeStateChange method just once when initializing 
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        private static void OnPlayModeStateChanged(PlayModeStateChange stateChange)
        {
            if (stateChange != PlayModeStateChange.ExitingPlayMode) return;
            // Locate all GameObjectRuntimeSetSO asset IDs
            var guids = AssetDatabase.FindAssets("t:GameObjectRuntimeSetSO");
            foreach (var guid in guids)
            {
                // Locate the asset by ID and path
                var path = AssetDatabase.GUIDToAssetPath(guid);

                var runtimeSet = AssetDatabase.LoadAssetAtPath<GameObjectRuntimeSetSO>(path);

                // Clear the Items list in each runtime set
                if (runtimeSet != null)
                {
                    runtimeSet.Clear();
                }
            }
        }

    }
}