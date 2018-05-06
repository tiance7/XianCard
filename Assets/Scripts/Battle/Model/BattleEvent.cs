using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleEvent
{
    BATTLE_EVENT_BEGIN = 5000,  //战斗事件枚举起始值

    //卡牌
    DRAW_ONE_CARD,   //抽一张卡
    DECK_NUM_UPDATE,    //抽牌堆数量更新
    USED_NUM_UPDATE,    //弃牌堆数量更新
    MOVE_HAND_TO_USED,  //从手牌移动到弃牌堆
    HAND_CARD_CONSUME,  //手牌使用掉了（本局无法再使用，不进消耗区）
    HAND_CARD_EXHAUST,  //手牌被消耗了

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
    ENEMY_ARMOR_CHANGE,
    ENEMY_BUFF_ADD,
    ENEMY_BUFF_UPDATE,
    ENEMY_BUFF_REMOVE,
}
