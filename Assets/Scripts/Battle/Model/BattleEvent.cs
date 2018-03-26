using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleEvent
{
    //卡牌
    DRAW_ONE_CARD,   //抽一张卡
    DECK_NUM_UPDATE,    //抽牌堆数量更新
    USED_NUM_UPDATE,    //弃牌堆数量更新
    MOVE_HAND_TO_USED,  //从手牌移动到弃牌堆
    HAND_CARD_CONSUME,  //手牌使用掉了（本局无法再使用，不进消耗区）

    //自身属性
    SELF_HP_UPDATE,
    SELF_DEAD,
    ARMOR_CHANGE,   //护甲改变
    SELF_BUFF_ADD,
    SELF_BUFF_UPDATE,
    SELF_BUFF_REMOVE,

    //全局
    COST_CHANGE,    //费用改变
    BOUT_UPDATE,    //回合改变

    //敌人
    ENEMY_INIT,
    ENEMY_HP_UPDATE,
    ENEMY_DEAD,
    ENEMY_ACTION_UPDATE,
}
