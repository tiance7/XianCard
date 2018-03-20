using System;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;

public static class UITool
{
    private static Dictionary<string, bool> _lstLoadedUIPackage = new Dictionary<string, bool>();
    private static Dictionary<string, List<Action>> _lstLoadingUIPackage = new Dictionary<string, List<Action>>();

    // 加载UI包(加载过的UI包不再进行重复加载)
    public static void LoadUIPackage(string packageName, Action onComplete = null, bool isChangeSceneAutoDispose = true)
    {
        // 已加载成功UiPackage直接回调
        if (_lstLoadedUIPackage.ContainsKey(packageName))
        {
            onComplete();
            return;
        }

        List<Action> _callBackList;

        // 正在加载UiPackage了就不用重复AddPackage，只要等AddPackage完成一起执行CallBack回调。
        if (_lstLoadingUIPackage.ContainsKey(packageName))
        {
            _lstLoadingUIPackage.TryGetValue(packageName, out _callBackList);

            if (_callBackList != null && _callBackList.Count > 0)
            {
                if (!_callBackList.Contains(onComplete))
                {
                    _callBackList.Add(onComplete);
                    return;
                }
            }
        }

        _callBackList = new List<Action>();
        _callBackList.Add(onComplete);
        _lstLoadingUIPackage.Add(packageName, _callBackList);

        UIPackage.AddPackage(ResPath.UI + packageName);
        _lstLoadedUIPackage.Add(packageName, isChangeSceneAutoDispose);
        AddUiPackageCompleted(packageName);

        //#if UNITY_EDITOR
        //        if (Core.Editor)
        //        {

        //            UIPackage.AddPackage(GamePath.UI + packageName, (name, extension, type) => UnityEditor.AssetDatabase.LoadAssetAtPath(name + extension, type));
        //            _lstLoadedUIPackage.Add(packageName, isChangeSceneAutoDispose);
        //            AddUiPackageCompleted(packageName);
        //            return;
        //        }
        //#endif

        //        AssetManager.LoadAssetBundle(GamePath.UI + packageName, ab =>
        //        {
        //            if (ab == null)
        //            {
        //                Debug.LogWarning("Loading uipackage error:" + packageName);
        //                return;
        //            }
        //            UIPackage.AddPackage(ab);
        //            _lstLoadedUIPackage.Add(packageName, isChangeSceneAutoDispose);
        //            AddUiPackageCompleted(packageName);
        //        });
    }

    // AddPackage完成，通知界面可以打开
    private static void AddUiPackageCompleted(string packageName)
    {
        List<Action> _callBackList;
        _lstLoadingUIPackage.TryGetValue(packageName, out _callBackList);

        if (_callBackList == null || _callBackList.Count == 0)
            return;

        foreach (Action _callBack in _callBackList)
        {
            if (_callBack != null)
                _callBack();
        }

        _lstLoadingUIPackage.Remove(packageName);
        _callBackList.Clear();
        _callBackList = null;
    }

    // 为对象创建UI
    public static void CreateUI(GameObject parent, GComponent ui)
    {
        var container = new Container(parent);
        container.renderMode = RenderMode.WorldSpace;
        container.touchable = false;
        container.touchChildren = false;
        //container.fairyBatching = true;
        container.AddChild(ui.displayObject);
        Stage.inst.AddChild(container);
    }

    // 在切换场景时候，自动释放标识为可释放的UI包
    public static void RemoveAllUiPackage()
    {
        List<string> lstRemovePackage = new List<string>();
        foreach (string packageName in _lstLoadedUIPackage.Keys)
        {
            if (!_lstLoadedUIPackage[packageName])
                continue;
            lstRemovePackage.Add(packageName);
        }
        foreach (string packageName in lstRemovePackage)
        {
            RemovePackage(packageName);
        }
    }

    public static void RemovePackage(string packageName)
    {
        if (_lstLoadedUIPackage.ContainsKey(packageName))
            _lstLoadedUIPackage.Remove(packageName);
        UIPackage.RemovePackage(packageName, true);
//#if UNITY_EDITOR
//        if (Core.Editor)
//            return;
//#endif
//        AssetManager.DisposeAssetBundle(GamePath.UI + packageName);
    }

}
