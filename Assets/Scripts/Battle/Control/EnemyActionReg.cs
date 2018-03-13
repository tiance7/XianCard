using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActionReg
{
    public static Dictionary<uint, Type> dicAction = new Dictionary<uint, Type>();

    /// <summary>
    /// 所有敌人逻辑绑定关系在这里初始化
    /// </summary>
    public static void InitAction()
    {
        dicAction.Add(1, typeof(Enemy1));
    }

    /// <summary>
    /// 获取敌人逻辑控制器
    /// </summary>
    /// <param name="tplId"></param>
    /// <returns></returns>
    public static IEnemy GetAction(uint tplId)
    {
        if(dicAction.ContainsKey(tplId))
        {
            Type type = dicAction[tplId];
            return Activator.CreateInstance(type) as IEnemy;
        }
        Debug.LogError("unhandle enemy tpl id:" + tplId);
        return null;
    }

}
