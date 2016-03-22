using UnityEditor;
using UnityEngine;

namespace TextureInfoEx
{
    [CustomEditor(typeof(TextureDataStorage))]
    public class TextureDataStorageEditor: Editor
    {
        bool _confirmation;
        public override void OnInspectorGUI()
        {
            var storage = target as TextureDataStorage;
            if (storage == null) return;
            EditorGUILayout.HelpBox(string.Format("{0} entries", storage.EditorData.Count), MessageType.Info);
            if (!_confirmation)
            {
                if (GUILayout.Button("Clear All"))
                {
                    _confirmation = true;
                }
            }
            else
            {
                EditorGUILayout.HelpBox("Are you sure?\nAll data will be lost!", MessageType.Info);
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("Cancel"))
                {
                    _confirmation = false;
                }
                if (GUILayout.Button("Clear All!"))
                {
                    storage.EditorData.Clear();
                    _confirmation = false;
                }
                EditorGUILayout.EndHorizontal();
            }
        }
    }
}
