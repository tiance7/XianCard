using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectType
{
    public const int NONE = 0;                      
    public const int PLAYER = 1;                       //Íæ¼Ò
    public const int ENEMY = 2;                        //µÐÈË
}

public class ObjectBase
{
    public int objType { get; private set; }
    public int instId { get; private set; }
    public int curHp { get; set; }
    public int maxHp;
    public int armor;
    public List<BuffInst> lstBuffInst = new List<BuffInst>();

    public ObjectBase(int objType)
    {
        this.objType = objType;
        instId = BattleTool.GetObjectInstId();
        curHp = 0;
        maxHp = 0;
        armor = 0;
    }

    public bool HasBuff(uint buffType)
    {
        foreach (var buffInst in lstBuffInst)
        {
            BuffTemplate template = BuffTemplateData.GetData(buffInst.tplId);
            if (template == null)
                continue;
            if (template.nType == buffType)
                return true;
        }
        return false;
    }

    public BuffInst GetBuffInst(uint buffId)
    {
        foreach (var buffInst in lstBuffInst)
        {
            if (buffInst.tplId == buffId)
                return buffInst;
        }

        return null;
    }
}