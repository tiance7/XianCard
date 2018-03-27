using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffectType
{
    public const uint ONE_DAMAGE = 1;                   //1次伤害
    public const uint GET_ARMOR = 2;                    //获得护甲
    public const uint CASTER_GET_BUFF = 3;              //释放者获得buff
    public const uint DRAW_CARD = 4;                    //抽牌堆抽取牌堆顶的卡牌
    public const uint GIVE_COST = 5;                    //获得能量
    public const uint CONSUME_BUFF_GET_BUFF = 6;        //消耗指定BUFF，获得对应效果值得另一个BUFF
}
