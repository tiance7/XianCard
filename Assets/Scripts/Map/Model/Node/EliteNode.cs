using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 精英节点
/// </summary>
public class EliteNode : MapNodeBase
{
    public uint tplId;   //敌人ID todo 改成列表或者根据需求重构以支持多个怪物初始化

    public EliteNode(uint tplId) : base(MapNodeType.ELITE)
    {
        this.tplId = tplId;
    }
}
