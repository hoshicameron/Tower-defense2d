using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AudioDelegateSO), true)]
public class AudioDelegateEditor : Editor
{
    private AudioSource previwer;

    private void OnEnable()
    {
        previwer = EditorUtility
            .CreateGameObjectWithHideFlags("Audio Preview", HideFlags.HideAndDontSave,
                typeof(AudioSource)).GetComponent<AudioSource>();

    }

    private void OnDisable()
    {
        DestroyImmediate(previwer.gameObject);
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EditorGUI.BeginDisabledGroup(serializedObject.isEditingMultipleObjects);
        if (GUILayout.Button("Preview"))
        {
            ((AudioDelegateSO) target).Play(previwer);
        }
        EditorGUI.EndDisabledGroup();
    }
}