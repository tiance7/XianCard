using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MsgType
{
    DO_ATTACK,  //进行攻击
    ATTACK_HIT,  //攻击命中
    SHOW_HIT_EFFECT, //显示受击特效
    BATTLE_END,  //战斗结束
    FIGHTER_ROLL_OVER,   //鼠标移入战斗者
    FIGHTER_ROLL_OUT    //鼠标移出战斗者
}
