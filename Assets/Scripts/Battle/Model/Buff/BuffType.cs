using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// buff类型
/// </summary>
public class BuffType
{
    public const uint ARMOR_REFLECT = 1;                //反伤=护甲减少值*系数
    public const uint ADD_ARMOR_ROUND_END = 2;          //回合结束获得护甲
    public const uint KEEP_ARMOR = 3;                   //回合结束保留护甲
    public const uint CAN_NOT_DRAW_CARD = 4;            //无法抽卡
    public const uint MULTI_ARMOR = 5;                  //多重护甲

    public const uint GET_ENERGY_ROUND_START = 9;       //回合开始获得能量
    public const uint GET_BUFF_ROUND_START = 10;        //回合开始获得BUFF

    public const uint DEBUFF_BEGIN = 80;
    public const uint WEAK = 81;                         //虚弱（攻击伤害减少）
    public const uint VULNERABLE = 82;                   //易伤（受到伤害增加）
    public const uint FRAIL = 83;                        //脆弱（护甲增加值减少）
    public const uint DEBUFF_END = 84;

    //剑修相关
    public const uint MAGIC_SWORD = 101;                //幻剑
}

/// <summary>
/// buff触发类型
/// </summary>
public class BuffTriggerType
{
    public const uint NONE = 0;                         //无
    public const uint ON_HIT = 1;                       //受击时
    public const uint BOUT_END = 2;                     //回合结束
    public const uint BOUT_START = 3;                   //回合开始
    public const uint BOUT_DRAW_END = 4;                //回合抽牌结束
    public const uint DO_HARM = 5;                      //造成伤害时
    public const uint CARD_EXHAUST = 6;                 //卡牌被消耗时

}