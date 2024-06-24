using UnityEditor;
using UnityEngine;

namespace Modules.TowerModule.Scripts.Internal.ScriptableObjects.Datas.Editor
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