# TextureInfoEx
## About
Unity (Editor & Runtime) extension to associate various information with Textures (Texture2D).
For example the type of material displayed (wood, stone, etc.).
Similar to tagging them (currently not even possible in Unity itself), only
that you can associate any type of data Unity can handle in the editor with it.
This information can then be retrieved at runtime from the texture reference.

## Usage
#### Editor
Open the window via **Edit** -> **Texture Data**, select your texture and edit the appropriate fields.
Hit 'Save' to mark the changes dirty (notify Unity that they need to be saved).

#### InGame
*Example* (c#):
```csharp
var myData;
if ([textureReference].GetUserData(out myData))
{
  // Do Something with myData;
}
```

### Extend
The **TextureData** class can be edited to add or remove properties.

### Notes
Multi-Selection editing is currently not implemented.
Undo/Redo does not work as expected (Unity's fault).
