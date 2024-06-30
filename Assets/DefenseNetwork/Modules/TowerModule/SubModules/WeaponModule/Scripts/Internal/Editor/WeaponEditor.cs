using UnityEditor;
using UnityEngine;

namespace DefenseNetwork.Modules.TowerModule.SubModules.WeaponModule.Scripts.Internal.Editor
{
    [CustomEditor(typeof(Weapon))]
    public class WeaponEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var weapon = (Weapon)target;
            EditorGUILayout.Space();
            if(GUILayout.Button("Setup Weapon Renderer"))
                weapon.SetupWeaponRenderer();
        }
    }
}