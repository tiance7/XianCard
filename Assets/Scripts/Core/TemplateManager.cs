using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

public sealed class TemplateManager
{
    private readonly TemplateList _list;

    public TemplateManager(TemplateList list)
    {
        _list = list;
    }

    public void LoadAllXml()
    {

        List<string> xmlNameList = _list.XMLList;
        List<Action<XmlNode>> funcList = _list.FunctionList;
        if (xmlNameList == null || funcList == null)
            return;

        for (int i = 0; i < xmlNameList.Count; i++)
        {
            string fileName = Path.GetFileNameWithoutExtension(xmlNameList[i]);
            string filePath = ResPath.XML_DIR + fileName;
            TextAsset textAsset = AssetManager.Load<TextAsset>(filePath);
            if (textAsset == null)
            {
                Debug.LogWarning("没有" + xmlNameList[i]);
                continue;
            }
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(textAsset.text);

            XmlNode xmlNode = xmlDoc.LastChild.FirstChild.FirstChild;
            funcList[i](xmlNode);
        }
    }

    // 加载单个XML
    public void LoadSingleXml(string path, string xmlName, Action<XmlNode> callback)
    {
        string fileName = Path.GetFileNameWithoutExtension(xmlName);
        string filePath = ResPath.XML_DIR + fileName;
        TextAsset textAsset = AssetManager.Load<TextAsset>(filePath);
        if (textAsset == null)
        {
            Debug.Log("LoadError：" + xmlName);
            return;
        }

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);

        XmlNode xmlNode = xmlDoc.LastChild.FirstChild.FirstChild;
        callback(xmlNode);
    }
}