using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    BoutAction GetNextBoutAction(); //获取下一回合行动
}

public class BoutAction
{
    public EnemyAction enemyAction;
    public int iValue;
}

public enum EnemyAction
{
    ATTACK
}