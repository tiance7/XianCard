using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 法宝基类
/// </summary>
public class RelicBase
{
    public uint instId { get; private set; }    //实例ID
    public uint tplId { get; private set; }     //模板ID
    public uint effectVal { get; private set; } //法宝数值

    public RelicBase(uint tplId)
    {
        instId = CardTool.GetUniCardInstId();
        this.tplId = tplId;
    }
    
    public virtual void OnGetRelic() { return; }

    public virtual void OnCharBoutEnd() { return; }

    public virtual void OnEnemyBoutEnd() { return; }

    public virtual void OnObjectLoseHP() { return; }

    public virtual void OnCharUseCardEnd() { return; }

    public virtual void OnObjectAddBuff() { return; }

}
