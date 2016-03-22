using TextureInfoEx;
using UnityEditor;

public class TextureDeleteHandler: AssetPostprocessor //AssetModificationProcessor
{
    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        var storage = TextureDataStorage.Load();
        for (var i = storage.EditorData.Count; i-- > 0;)
        {
            if (storage.EditorData[i].TextureReference == null)
            {
                storage.EditorData.RemoveAt(i);
            }
        }
    }
}
