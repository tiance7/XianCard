using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 单次（卡牌）效果数据统计
/// </summary>
public class EffectStatistics
{
    public List<CardInstance> lstExhaustCard = new List<CardInstance>();      //被消耗的卡牌列表
    public List<CardInstance> lstDrawCard = new List<CardInstance>();         //被抽到的卡牌列表
    public bool killEnemy;                                      //是否击杀敌方
    public uint damageLife;                                     //(对敌方)伤害生命值
    public uint damageArmor;                                    //(对敌方)消耗护甲值
    public uint getCostCount;                                   //获得能量数
    public uint consumeCost;                                    //消耗的能量

    public EffectStatistics()
    {
        damageLife = 0;
        damageArmor = 0;
        getCostCount = 0;
        consumeCost = 0;
    }
}

/// <summary>
/// 回合数据统计
/// </summary>
public class RoundStatistics
{
    public uint drawCardCount;                                  //额外抽牌数
    public uint disCardCount;                                   //弃牌数
    public uint exhaustCardCount;                               //消耗牌数
    public uint getCostCount;                                   //获得能量数
    public uint consumeCost;                                    //消耗的能量
    public uint damageLife;                                     //(对敌方)伤害生命值
    public uint damageArmor;                                    //(对敌方)消耗护甲值
    public List<CardInstance> lstUsedCard = new List<CardInstance>();     //打出的卡牌列表
    public List<CardInstance> lstExhaustCard = new List<CardInstance>();  //被消耗的卡牌列表
    public List<CardInstance> lstDrawCard = new List<CardInstance>();     //被抽到的卡牌列表

    public RoundStatistics()
    {
        drawCardCount = 0;
        disCardCount = 0;
        exhaustCardCount = 0;
        getCostCount = 0;
        consumeCost = 0;
        damageLife = 0;
        damageArmor = 0;
    }
}

/// <summary>
/// 战斗数据统计
/// </summary>
public class BattleStatistics
{
    public uint drawCardCount;              //额外抽牌数
    public uint disCardCount;               //弃牌数
    public uint exhaustCardCount;           //消耗牌数
    public uint useCardCount;               //使用卡牌数
    public uint getCostCount;               //获得能量数 
    public uint injuredCount;               //受伤次数

    public BattleStatistics()
    {
        drawCardCount = 0;
        disCardCount = 0;
        exhaustCardCount = 0;
        useCardCount = 0;
        getCostCount = 0;
        injuredCount = 0;
    }
}