using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffectType
{
    public const uint NONE = 0;
    public const uint ONE_DAMAGE = 1;                   //1次伤害
    public const uint GET_ARMOR = 2;                    //获得护甲
    public const uint GIVE_BUFF = 3;                    //获得buff
    public const uint DRAW_CARD = 4;                    //抽牌堆抽取牌堆顶的卡牌
    public const uint GIVE_COST = 5;                    //获得能量
    public const uint CONSUME_BUFF_GET_BUFF = 6;        //消耗指定BUFF，获得对应效果值得另一个BUFF
    public const uint ONE_DAMAGE_IGNORE_ARMOR = 7;      //无视护甲，造成1次伤害
    public const uint DAMAGE_SELF = 8;                  //对自身造成伤害
    public const uint DRAW_CARD_UNTIL_NOTATTACK = 9;    //抽牌，直到抽到的牌不是攻击牌
    public const uint ARMOR_DAMAGE = 10;                //造成与护甲值相同的伤害
    public const uint CLEAR_SELF_ARMOR = 11;            //清除自身所有护甲
    public const uint RECOVER_HP_BY_DAMAGE = 12;        //根据伤害值回复血量
    public const uint CARD_EFFECT_MAX = 13;
}
