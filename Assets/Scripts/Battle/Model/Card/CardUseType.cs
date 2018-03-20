using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 卡牌使用条件
/// </summary>
public class CardUseType
{
    public const uint NONE = 0;                              //无特殊使用限制
    public const uint CAN_NOT_PLAY = 1;                      //无法被打出

    public const uint ALL_ATTACK_IN_HAND = 11;               //手牌都是攻击牌
    public const uint ALL_SKILL_IN_HAND = 12;                //手牌都是技能牌
    public const uint ALL_FORMATION_IN_HAND = 13;            //手牌都是阵法牌
    public const uint UP_ATTACK_IN_HAND = 14;                //手牌有X张以上攻击牌
    public const uint UP_SKILL_IN_HAND = 15;                 //手牌有X张以上技能牌
    public const uint UP_FORMATION_IN_HAND = 16;             //手牌有X张以上阵法牌
    public const uint ODD_CARD_IN_HAND = 17;                 //手牌都是奇数费用卡
    public const uint EVEN_CARD_IN_HAND = 18;                //手牌都是偶数费用卡


    public const uint DECK_CARD_COUNT = 30;                  //抽牌堆剩余X张卡
    public const uint DECK_ODD_COUNT = 31;                   //抽牌堆剩余奇数张卡
    public const uint DECK_EVEN_COUNT = 32;                  //抽牌堆剩余偶数张卡

    public const uint HAVE_BUFF_TYPE = 40;                   //身上有指定类型的BUFF
}