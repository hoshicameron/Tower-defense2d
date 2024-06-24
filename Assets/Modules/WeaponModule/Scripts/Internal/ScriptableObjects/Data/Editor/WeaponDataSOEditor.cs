using UnityEditor;
using UnityEngine;

namespace Modules.WeaponModule.Scripts.Internal.ScriptableObjects.Data.Editor
{
    [CustomEditor(typeof(WeaponDataSO))]
    public class WeaponDataSOEditor : UnityEditor.Editor
    {
        private WeaponDataSO weaponDataSo;

        private void OnEnable()
        {
            weaponDataSo = target as WeaponDataSO;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if(weaponDataSo.Sprite == null)
                return;

            var texture = AssetPreview.GetAssetPreview(weaponDataSo.Sprite);
            GUILayout.Space(10f);
            GUILayout.Label("Sprite Preview:");
            GUILayout.Label("",GUILayout.Height(80f),GUILayout.Width(80f));
            GUI.DrawTexture(GUILayoutUtility.GetLastRect(),texture);
        }
    }
}