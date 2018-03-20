using System;
using UnityEngine;

public sealed class AssetManager
{
    public static T Load<T>(string path) where T : UnityEngine.Object
    {
        return Resources.Load<T>(path);
    }

    /// <summary>
    /// 加载纹理
    /// </summary>
    /// <param name="imgPath"></param>纹理集路径
    /// <param name="imageName"></param>图片在纹理集中的名称
    /// <param name="callback"></param>
    public static void LoadAtlas(string imgPath, Action<Sprite> callback, string imageName = null)
    {
        Sprite sprite = null;
        if (string.IsNullOrEmpty(imageName))
        {
            sprite = Resources.Load<Sprite>(imgPath);
        }
        else
        {
            UnityEngine.Object[] sprites = Resources.LoadAll(imgPath);
            for (int i = 0; i < sprites.Length; ++i)
            {
                if (sprites[i].name != imageName)
                    continue;
                sprite = (Sprite)sprites[i];
                break;
            }
        }
        if (sprite == null)
        {
            Debug.LogError("load atlas error" + imgPath);
            if (!string.IsNullOrEmpty(imageName))
                Debug.LogError("error imageName " + imageName);
            callback(sprite);
            return;
        }
        callback(sprite);
    }

}
