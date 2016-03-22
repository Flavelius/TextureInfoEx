using TextureInfoEx;
using UnityEngine;

public static class TextureInfoExtension
{
    static TextureDataStorage _cachedDataStorage;

    public static bool GetUserData(this Texture2D reference, out TextureData data)
    {
        if (ReferenceEquals(_cachedDataStorage, null))
        {
            _cachedDataStorage = TextureDataStorage.Load();
        }
        return _cachedDataStorage.GetInfoFor(reference, out data);
    }

    public static bool GetUserData(this Texture reference, out TextureData data)
    {
        if (ReferenceEquals(_cachedDataStorage, null))
        {
            _cachedDataStorage = TextureDataStorage.Load();
        }
        return _cachedDataStorage.GetInfoFor(reference, out data);
    }
}
