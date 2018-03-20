using FairyGUI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WindowManager
{
    private static readonly Dictionary<WindowId, WindowData> _datas = new Dictionary<WindowId, WindowData>();
    private static readonly Dictionary<WindowId, WindowExtend> _openedList = new Dictionary<WindowId, WindowExtend>();
    private static readonly Dictionary<WindowId, WindowExtend> _openingList = new Dictionary<WindowId, WindowExtend>();
    private static readonly Dictionary<WindowId, WindowId> _hideWindowList = new Dictionary<WindowId, WindowId>();

    // id - 由WindowId类定义
    // packageName - ui包资源名，对应FairyGUI发布设置的文件名，需要每个包自定义，必须使用全小写，FairyGUI发布设置可首字母大写（AssetBundle打包后资源名全变成小写）
    // windowType - 对应的Window类，必须继承WindowExtend，如LoginWindow
    // skinType - FairyGUI皮肤文件，用于直接使用组件name，如UILogin
    // bindType - FairyGUI中自定义组件，FairyGUI自动生成代码，如登录模块的DengLuBinder
    // canClose - 当切换场景的时候（如主界面进入战场会关闭所有窗口），是否允许关闭此窗口
    // FitScreen - 窗口在屏幕的适配类型： FitScreen.Center 窗口居中 FitScreen.FullScreen 扩展界面全屏（例如主界面）
    public static void Add(WindowId id, string packageName, Type windowType, Type skinType, bool canClose = true)
    {
        if (_datas.ContainsKey(id))
        {
            Debug.LogWarning("Registed " + id);
            return;
        }

        var data = new WindowData
        {
            Id = id,
            PackageName = packageName,
            WindowType = windowType,
            SkinType = skinType,
            CanClose = canClose,
        };

        _datas.Add(id, data);
    }

    public static void Remove(WindowId id)
    {
        if (Opened(id))
            Close(id);

        _datas.Remove(id);
    }

    public static bool Opened(WindowId id)
    {
        return _openedList.ContainsKey(id);
    }

    public static bool Opening(WindowId id)
    {
        return _openingList.ContainsKey(id);
    }

    public static Window Get(WindowId id)
    {
        if (_openedList.ContainsKey(id))
            return _openedList[id];
        if (_openingList.ContainsKey(id))
            return _openingList[id];
        return null;
    }

    public static void Open(WindowId openId, Action openComplete = null, params object[] values)
    {
        if (Opened(openId) || Opening(openId))
            return;

        if (!_datas.ContainsKey(openId))
        {
            Debug.LogWarning("Window not regist：" + openId);
            return;
        }

        var data = _datas[openId];
        WindowExtend window = (WindowExtend)Activator.CreateInstance(data.WindowType, values);
        _openingList[openId] = window;

        window.skinType = data.SkinType;
        window.windowId = data.Id;

        UITool.LoadUIPackage(data.PackageName, () =>
        {
            OpenWindowHandler(data, openId, openComplete);
        });
    }

    // 打开一个窗口，并且隐藏指定窗口，在关闭本窗口时恢复被隐藏窗口的显示
    public static void OpenAndHideOther(WindowId openID, WindowId hideID, Action onComplete = null, params object[] values)
    {
        if (Opened(hideID))
        {
            _hideWindowList.Add(openID, hideID);
            HideWindow(hideID, true);
        }

        Open(openID, onComplete, values);
    }

    private static void OpenWindowHandler(WindowData data, WindowId id, Action onComplete)
    {
        WindowExtend window = _openingList[id];
        window.Show();

        _openedList[id] = window;
        _openingList.Remove(id);

        if (onComplete != null)
            onComplete();
    }

    public static void Close(WindowId id)
    {
        if (Opening(id))
        {
            Debug.LogWarning("正在打开的窗口不能关闭，需要停止加载:" + id);
            return;
        }
        if (!Opened(id))
            return;

        // 打开窗口时有隐藏其他窗口，关闭时要恢复其他窗口的显示
        if (_hideWindowList.ContainsKey(id))
        {
            WindowId _hideID = _hideWindowList[id];
            HideWindow(_hideID, false);
            _hideWindowList.Remove(id);
        }

        WindowExtend window = _openedList[id];
        GRoot.inst.HideWindowImmediately(window, true);

        _openedList.Remove(id);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="disposePackage">关闭窗口时销毁UI包</param>
    public static void CloseAllWindow(Boolean disposePackage = false)
    {
        var closeList = new List<WindowId>();
        foreach (var pair in _openedList)
        {
            var data = _datas[pair.Key];
            if (!data.CanClose)
                continue;
            closeList.Add(pair.Key);
        }
        foreach (var id in closeList)
        {
            Close(id);
        }

        if (disposePackage)
            UITool.RemoveAllUiPackage();
    }

    private static void HideWindow(WindowId id, bool isHide)
    {
        WindowExtend window;
        _openedList.TryGetValue(id, out window);

        if (window == null)
            return;

        if (isHide)
            window.HideImmediately();
        else
            window.Show();
    }

    public static bool HasWindowOpened()
    {
        return _openedList.Count > 0 || _openingList.Count > 0;
    }
}

public struct WindowData
{
    public WindowId Id;
    public string PackageName;    // UI资源包名 （FairyGui中的包名）
    public Type WindowType;
    public Type SkinType;
    public bool CanClose;        // 切换场景时候，是否自动关闭
}