using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleDefine
{
    public const float CARD_SCALE = 0.7f;   //卡牌缩放比例
}

public enum Bout
{
    SELF,
    ENEMY
}

/// <summary>
/// 动画时间
/// </summary>
public class AnimationTime
{
    public const float HAND_TO_USED = 0.5f; //手牌飞入弃牌堆时间
}