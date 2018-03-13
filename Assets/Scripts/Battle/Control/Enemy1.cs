using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : IEnemy
{
    private BoutAction _boutAction;

    public Enemy1()
    {
        _boutAction = new BoutAction() { enemyAction = EnemyAction.ATTACK, iValue = 6 };
    }

    public BoutAction GetNextBoutAction()
    {
        return _boutAction;
    }
}
