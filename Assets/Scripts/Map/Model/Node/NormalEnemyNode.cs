using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 普通怪物节点
/// </summary>
public class NormalEnemyNode : MapNodeBase
{
    public uint tplId;   //敌人ID todo 改成列表或者根据需求重构以支持多个怪物初始化

    public NormalEnemyNode(uint tplId) : base(MapNodeType.NORMAL_ENEMY)
    {
        this.tplId = tplId;
    }
}
