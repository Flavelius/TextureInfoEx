using System;
using System.Collections.Generic;
using UnityEngine;

namespace TextureInfoEx
{
    public class TextureDataStorage : ScriptableObject
    {
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        [SerializeField] List<EditorDataAssociation> _editorData = new List<EditorDataAssociation>();
        public List<EditorDataAssociation> EditorData { get { return _editorData; } } 
        readonly Dictionary<Texture, TextureData> _runtimeData = new Dictionary<Texture, TextureData>();

        public static TextureDataStorage Load()
        {
            var storate = Resources.Load<TextureDataStorage>("TextureDataStorage");
            if (ReferenceEquals(storate, null))
            {
                Debug.LogError("TextureDataStorage asset could not be loaded, no data will be available");
                return CreateInstance<TextureDataStorage>();
            }
            return storate;
        }

        public bool GetInfoFor(Texture reference, out TextureData data)
        {
            if (ReferenceEquals(reference, null))
            {
                data = null;
                return false;
            }
            if (Application.isPlaying)
            {
                if (_runtimeData.Count == 0)
                {
                    for (var i = 0; i < EditorData.Count; i++)
                    {
                        if (EditorData[i].TextureReference == null | EditorData[i].AssociatedData == null) continue;
                        _runtimeData.Add(EditorData[i].TextureReference, EditorData[i].AssociatedData);
                    }
                }
                return _runtimeData.TryGetValue(reference, out data);
            }
            for (var i = EditorData.Count; i-->0;)
            {
                if (EditorData[i].TextureReference == reference)
                {
                    data = EditorData[i].AssociatedData;
                    return true;
                }
            }
            data = null;
            return false;
        }

#if UNITY_EDITOR
        public bool AddInfoOrUpdateExisting(Texture reference, TextureData data)
        {
            if (Application.isPlaying)
            {
                Debug.LogWarning("Adding texture data at runtime is not supported");
                return false;
            }
            for (var i = 0; i < EditorData.Count; i++)
            {
                if (EditorData[i].TextureReference != reference) continue;
                EditorData[i].AssociatedData = data;
                return true;
            }
            EditorData.Add(new EditorDataAssociation(reference, data));
            return true;
        }
#endif

        [Serializable]
        public class EditorDataAssociation
        {
            public Texture TextureReference;
            public TextureData AssociatedData;

            public EditorDataAssociation(Texture texture, TextureData data)
            {
                TextureReference = texture;
                AssociatedData = data;
            }
        }
    }
}
