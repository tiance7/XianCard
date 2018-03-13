using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// buff类型
/// </summary>
public class BuffType
{
    public const uint ARMOR_REFLECT = 1;    //反伤=护甲减少值
    public const uint ADD_ARMOR = 2;    //获得护甲
    public const uint KEEP_ARMOR = 3;    //回合结束保留护甲
}

/// <summary>
/// buff触发类型
/// </summary>
public class BuffTriggerType
{
    public const uint ON_HIT = 1;    //受击时
    public const uint BOUT_END = 2;    //回合结束
}