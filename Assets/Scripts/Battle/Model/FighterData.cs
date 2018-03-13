using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterData
{
    public int instId;
    public int curHp { get; private set; }
    public int maxHp;
    public int armor;
    public List<BuffInst> lstBuffInst = new List<BuffInst>();

    public FighterData(int instId, int curHp, int maxHp, int armor)
    {
        this.instId = instId;
        this.curHp = curHp;
        this.maxHp = maxHp;
        this.armor = armor;
    }

    internal void ReduceHp(int value)
    {
        curHp -= value;
        if (curHp < 0)
            curHp = 0;
    }

    internal void BattleInit()
    {
        lstBuffInst.Clear();
    }
}
