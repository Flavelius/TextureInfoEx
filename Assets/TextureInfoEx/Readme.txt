Under the unity menu 'Edit' the entry 'Texture Data' opens the window which, when a texture is selected, shows the linked data.
Dont forget to hit save, when done editing.
In your scripts, just use the following way to access it (c# example):

var myData;
if ([YourTexture].GetUserData(out myData))
{
	//do stuff with it.
}

if it returns false, no data was associated with the texture, or the data storage could not be loaded 
(in this case it prints an error message to the console, but that shouldn't happen).

An example scene and some example scripts can be found in the 'Test' folder of the package.
You can swap the textures in the materials at runtime and the example components should pick up the change.
__________________________________________________________________________________

To add other data types or remove existing ones that are not needed, adjust the TextureData class.
Multi editing is currently not supported, but should not be hard to add. 
Important: Undo/redo is seemingly not possible (seems like a unity bug).