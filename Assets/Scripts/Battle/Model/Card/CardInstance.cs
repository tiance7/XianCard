using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 卡牌实例
/// </summary>
public class CardInstance
{
    public uint instId { get; private set; }    //卡牌实例ID
    public uint tplId { get; private set; }     //模板ID
    public uint cardType { get; set; }          //卡牌类型 CardType

    public CardInstance(uint tplId)
    {
        instId = CardTool.GetUniCardInstId();
        this.tplId = tplId;

        this.cardType = CardTemplateData.GetData(tplId).nType;
    }

    public CardInstance Clone()
    {
        return this.MemberwiseClone() as CardInstance;
    }
}
