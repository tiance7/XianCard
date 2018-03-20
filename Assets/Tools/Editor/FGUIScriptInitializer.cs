using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FGUIScriptInitializer : UnityEditor.AssetModificationProcessor
{
    public static void OnWillCreateAsset(string path)
    {
        path = path.Replace(".meta", "");
        if (path.ToLower().EndsWith(".cs"))
        {
            string fileName = Path.GetFileNameWithoutExtension(path);

            string content = File.ReadAllText(path);
            Debug.Log(path);

            string windowName = fileName.Replace("Window", "");
            content = content.Replace("#WINDOWNAME#", windowName);


            File.WriteAllText(path, content);
        }
    }
}
