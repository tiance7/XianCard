/***************************
 * Author:      luwenzhen
 * CreateTime:  10/11/2017
 * LastEditor:
 * Description:
 * 
**/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TextTool
{
	/// <summary>
    /// color��ʽ "#FFFFFF"
    /// </summary>
    /// <param name="txt"></param>
    /// <param name="color"></param>
    /// <returns></returns>
    public static string FormatUBBColor(string txt, string color)
    {
        return string.Format("[color={0}]{1}[/color]", color, txt);
    }

    public static string ReplaceAll(string content, string pattern, object repl)
    {
        while (content.IndexOf(pattern) != -1)
        {
            content = content.Replace(pattern, repl.ToString());
        }
        return content;
    }

}
