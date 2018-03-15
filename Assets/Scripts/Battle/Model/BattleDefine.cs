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
    public const float ATTACK_TIME = 1.63f; //攻击总时间(头顶1s + 攻击位移0.63s)
    public const float BOUT_DISPLAY_TIME = 1.5f; //回合展示时间
    public const float BOUT_END_TIME = 1.5f; //回合结束时间
}

/// <summary>
/// 攻击结构体
/// </summary>
public class AttackStruct
{
    public EnemyInstance casterInst;    //攻击者
    public BoutAction boutAction;

    public AttackStruct(EnemyInstance enemyInst, BoutAction boutAction)
    {
        this.casterInst = enemyInst;
        this.boutAction = boutAction;
    }
}
