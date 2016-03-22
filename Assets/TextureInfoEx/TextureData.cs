using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TextureInfoEx
{

    public enum TextureSurfaceType
    {
        Undefined,
        Wood,
        Stone,
        Cloth,
        Water,
        Snow,
        Grass,
        Ice,

    }

    [Serializable]
    public class TextureData
    {
        [SerializeField] string _tag = "";
        public string Tag { get { return _tag; } }
        [SerializeField] TextureSurfaceType _surfaceType = TextureSurfaceType.Undefined;
        public TextureSurfaceType SurfaceType { get { return _surfaceType; } }
        [SerializeField] Color _associatedColor = Color.white;
        public Color AssociatedColor { get { return _associatedColor; } }

        public TextureData()
        {
            _tag = "";
            _surfaceType = TextureSurfaceType.Undefined;
            _associatedColor = Color.white;
        }

        public TextureData(TextureData source)
        {
            _tag = source._tag;
            _surfaceType = source._surfaceType;
            _associatedColor = source._associatedColor;
        }

#if UNITY_EDITOR
        public void Reset()
        {
            _tag = "";
            _surfaceType = TextureSurfaceType.Undefined;
            _associatedColor = Color.white;
        }

        public void DrawGui()
        {
            _tag = EditorGUILayout.TextField("Tag", _tag);
            _surfaceType = (TextureSurfaceType)EditorGUILayout.EnumPopup("SurfaceType", _surfaceType);
            _associatedColor = EditorGUILayout.ColorField("Color", _associatedColor);
        }
#endif
    }
}
