using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectBase
{
    public int instId { get; private set; }
    public int curHp { get; set; }
    public int maxHp;
    public int armor;
    public List<BuffInst> lstBuffInst = new List<BuffInst>();

    public ObjectBase()
    {
        instId = BattleTool.GetObjectInstId();
        curHp = 0;
        maxHp = 0;
        armor = 0;
    }
}