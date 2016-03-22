using UnityEditor;
using UnityEngine;

namespace TextureInfoEx
{
    public class TextureDataEditorWindow : EditorWindow
    {
        Texture2D _selected;
        TextureData _data;
        TextureDataStorage _storage;

        void OnEnable()
        {
            if (_storage == null)
            {
                _storage = TextureDataStorage.Load();
            }
            OnSelectionChange();
        }

        void OnSelectionChange()
        {
            if (Selection.activeObject != _selected)
            {
                _selected = Selection.activeObject as Texture2D;
                if (!_selected.GetUserData(out _data))
                {
                    _data = new TextureData();
                }
                Repaint();
            }
        }

        void OnGUI()
        {
            if (Application.isPlaying)
            {
                EditorGUILayout.HelpBox("Editing disabled in PlayMode", MessageType.Info);
                GUI.enabled = false;
            }
            if (!_selected | _data == null)
            {
                GUILayout.Label("No Texture selected");
                return;
            }
            _data.DrawGui();
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Reset", GUILayout.ExpandWidth(false)))
            {
                _data.Reset();
            }
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Save", GUILayout.ExpandWidth(false)))
            {
                SaveActive();

            }
            EditorGUILayout.EndHorizontal();
        }

        void SaveActive()
        {
            Undo.RecordObject(_storage, "Texture Data edit");
            if (_storage.AddInfoOrUpdateExisting(_selected, _data))
            {
                EditorUtility.SetDirty(_storage);
            }
        }

        [MenuItem("Edit/Texture Data")]
        static void OpenWindow()
        {
            GetWindow<TextureDataEditorWindow>("Texture Data");
        }
    }
}
