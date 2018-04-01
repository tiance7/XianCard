using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// BUFF实例
/// </summary>
public class BuffInst
{
    public uint tplId;
    public int ownerInstId;
    public int leftBout;                    //剩余回合数 -1表示无限
    public int effectVal;                   //效果值
    public bool selfAddDebuffThisBout;      //是否是这回合自身释放的debuff，是的话不减少剩余回合数

    public static bool IsDebuff(uint buffType)
    {
        return buffType == BuffType.WEAK || buffType == BuffType.VULNERABLE || buffType == BuffType.FRAIL;
    }

    public BuffInst(int ownerInstId)
    {
        this.ownerInstId = ownerInstId;
        selfAddDebuffThisBout = false;
    }
}
