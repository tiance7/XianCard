using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MsgType
{
    DO_ATTACK,  //进行攻击
    ATTACK_HIT,  //攻击命中
    SHOW_HIT_EFFECT, //显示受击特效
    BATTLE_WIN,  //战斗胜利
    FIGHTER_ROLL_OVER,   //鼠标移入战斗者
    FIGHTER_ROLL_OUT,    //鼠标移出战斗者

    SELF_BOUT_END,  //玩家回合结束
}
