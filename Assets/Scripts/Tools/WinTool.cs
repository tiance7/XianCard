/***************************
 * Author:      luwenzhen
 * CreateTime:  9/21/2017
 * LastEditor:
 * Description:
 * 
**/
using System;
using System.Collections.Generic;
using UI.Battle;

public static class WinTool
{
	private static bool isRegiste = false;                   // 防止不同模块重复注册

	public static void RegisteWindows()
	{
		if (isRegiste)
			return;
		isRegiste = true;

        //-----------通用-----------
        InitWindowPackage(typeof(BattleBinder), PackageName.BATTLE,
            new List<Window> {
                new Window(WindowId.BATTLE_REWARD, typeof(BattleRewardWindow), typeof(BattleRewardFrame)),
            });

    }

	private static void InitWindowPackage(Type binderType, string packageName, List<Window> windowList)
	{
		binderType.GetMethod("BindAll").Invoke(null, null);
		foreach (Window window in windowList)
		{
			WindowManager.Add(window.windowId, packageName, window.windowType, window.frameType, window.canClose);
		}
	}

	class Window
	{
		public WindowId windowId;
		public Type windowType;
		public Type frameType;
		public bool canClose;

		public Window(WindowId windowId, Type windowType, Type frameType, bool canClose = true)
		{
			this.windowId = windowId;
			this.windowType = windowType;
			this.frameType = frameType;
			this.canClose = canClose;
		}
	}
}


