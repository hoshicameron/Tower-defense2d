using UnityEditor;
using UnityEngine;

namespace Tower.Scripts.Editor
{
    [CustomEditor(typeof(Tower))]
    public class TowerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var tower = (Tower)target;
            EditorGUILayout.Space();
            if(GUILayout.Button("Setup Renderers"))
                tower.SetupRenderers();
        }
    }
}