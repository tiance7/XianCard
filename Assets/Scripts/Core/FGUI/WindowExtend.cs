using System;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

public enum FitScreen
{
    None,
    Center,
    FullScreen
}

public class WindowExtend : Window
{
    private static List<WindowId> _lstCloseInModalWindowId = new List<WindowId>();

    public Type bindType { get; internal set; } // 对应自动生成的绑定文件(必要初始化的参数)
    public Type skinType { get; internal set; } // 对应自动生成的皮肤文件(必要初始化的参数)
    public WindowId windowId;   //窗口名称(必要初始化的参数)

    protected FitScreen fitType = FitScreen.Center;
    protected float modalAlpha = 0.7f;      //模态窗口alpha值
    protected bool closeInModal = false;    //是否点击模态层关闭

    private bool _isCloseHandle = false;

    public WindowExtend()
        : base()
    {
    }

    public override void Dispose()
    {
        if (closeInModal)
        {
            _lstCloseInModalWindowId.Remove(windowId);
            GRoot.inst.modalLayer.onClick.Remove(OnModalLayerClick);
        }
        base.Dispose();
    }

    protected override void OnInit()
    {
        base.OnInit();

        //bindType.GetMethod("BindAll").Invoke(null, null);
        if (skinType != null)
            contentPane = skinType.GetMethod("CreateInstance").Invoke(null, null) as GComponent;

        if (modal)
        {
            //模态透明度
            GRoot.inst.modalLayer.shape.color = (UIConfig.modalLayerColor.a == modalAlpha) ? UIConfig.modalLayerColor : new UnityEngine.Color(0f, 0f, 0f, modalAlpha);
            if (closeInModal)
            {
                _lstCloseInModalWindowId.Add(windowId);
                GRoot.inst.modalLayer.onClick.Add(OnModalLayerClick);
            }
        }

        switch (fitType)
        {
            case FitScreen.Center:
                Center();
                break;
            case FitScreen.FullScreen:
                MakeFullScreen();
                break;
            case FitScreen.None:
                //do nothing
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void OnModalLayerClick(EventContext context)
    {
        if (_lstCloseInModalWindowId.LastIndexOf(windowId) != (_lstCloseInModalWindowId.Count - 1))
            return;
        Close();
    }

    protected override void OnHide()
    {
        base.OnHide();
        if (_isCloseHandle)
            WindowManager.Close(windowId);
    }

    public void Close()
    {
        _isCloseHandle = true;
        Hide();
    }

}
