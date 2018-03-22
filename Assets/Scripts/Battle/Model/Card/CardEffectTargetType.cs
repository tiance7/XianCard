using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffectTargetType
{
    public const uint NONE = 0;                     //没有目标
    public const uint ONE_ENEMY = 1;                //指定敌人
    public const uint RANDOM_ENEMY = 2;             //随机敌人
    public const uint ALL_ENEMY = 3;                //所有敌人
    public const uint SELF = 4;                     //自己
    public const uint ATTACKER = 5;                 //攻击者
}