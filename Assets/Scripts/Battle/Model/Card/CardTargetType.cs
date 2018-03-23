using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 卡牌目标类型
/// </summary>
public class CardTargetType
{
    public const uint NONE = 0;                     //没有目标（或者随机目标）
    public const uint ONE_ENEMY = 1;                //指定敌人
    public const uint ALL_ENEMY = 3;                //所有敌人
    public const uint SELF = 4;                     //自己
}
