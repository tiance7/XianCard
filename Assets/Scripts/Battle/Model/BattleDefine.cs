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
    public const float BATTLE_END_DEAD_TIME = 1.0f;    //战斗结束死亡动画时间
}

/// <summary>
/// 职业划分
/// </summary>
public class Job
{
    public const uint NONE = 0;  //通用
    public const uint SWORD = 1; //剑修
}

/// <summary>
/// 攻击结构体
/// </summary>
public class AttackStruct
{
    public EnemyInstance casterInst;    //攻击者
    public BoutAction boutAction;
    public bool isBlock { get; private set; }    //是否格挡

    public AttackStruct(EnemyInstance enemyInst, BoutAction boutAction, bool isBlock)
    {
        this.casterInst = enemyInst;
        this.boutAction = boutAction;
        this. isBlock = isBlock;
    }
}

/// <summary>
/// 生命改变结构体
/// </summary>
public class HpUpdateStruct
{
    public int instId;
    public int changeValue; //变化值 加血为正，扣血为负

    public HpUpdateStruct(int instId, int changeValue)
    {
        this.instId = instId;
        this.changeValue = changeValue;
    }
}