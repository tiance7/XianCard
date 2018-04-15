using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 法宝基类
/// </summary>
public class RelicBase
{
    public bool enable = true;
    public uint instId { get; private set; }    //实例ID
    public uint tplId { get; private set; }     //模板ID
    public uint effectVal = 0;                  //法宝数值


    public RelicBase(uint tplId)
    {
        instId = CardTool.GetUniCardInstId();
        this.tplId = tplId;
    }
    
    public virtual void OnPutIntoRelicList() { return; }

    public virtual void OnDisable() { return; }

    public virtual void OnCharBoutEnd(object obj) { return; }

    public virtual void OnEnemyBoutEnd() { return; }

    public virtual void OnObjectLoseHP() { return; }

    public virtual void OnCharUseCardEnd() { return; }

    public virtual void OnObjectAddBuff() { return; }

}
