using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffectTrigType
{
    public const uint NONE = 0;                                             //没有触发条件
    public const uint PRE_EFFECT_CONSUME_CARD_TYPE = 1;                     //前置效果中消耗了指定类型的卡牌
    public const uint PRE_EFFECT_DRAW_CARD_TYPE = 2;                        //前置效果中抽到指定类型的卡牌

    public const uint DROP_CARD_IN_ROUND = 8;                               //当前回合内有过弃牌
    public const uint EXHAUST_CARD_IN_ROUND = 9;                            //当前回合内有消耗牌

    public const uint TARGET_WANT_ATTACK = 15;                              //目标有攻击意图
    public const uint TARGET_HAS_BUFF_TYPE = 16;                            //目标有指定类型的BUFF
    public const uint SELF_HAS_BUFF_TYPE = 17;                              //自身有指定类型的BUFF

    public const uint GIVE_NOT_BLOCK_DAMAGE = 25;                           //造成未被格挡的伤害
}