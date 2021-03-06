﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResPath
{
    public const string XML_DIR = "XML/";
    public const string UI = "UI/";

    public const string SFX_BATTLE_WIN = "Sound/win";
    public const string SFX_SPEAR = "Sound/spear";

    /// <summary>
    /// 获取FGUI里的资源路径
    /// </summary>
    /// <param name="packageName"></param>
    /// <param name="imgName"></param>
    /// <returns></returns>
    public static string GetUiImagePath(string packageName, string imgName)
    {
        return string.Format("ui://{0}/{1}", packageName, imgName);
    }
}
