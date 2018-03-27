using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// buff类型
/// </summary>
public class BuffType
{
    public const uint ARMOR_REFLECT = 1;                //反伤=护甲减少值*系数
    public const uint ADD_ARMOR = 2;                    //获得护甲
    public const uint KEEP_ARMOR = 3;                   //回合结束保留护甲
    public const uint CAN_NOT_DRAW_CARD = 4;            //无法抽卡
    public const uint MULTI_ARMOR = 5;                  //多重护甲

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