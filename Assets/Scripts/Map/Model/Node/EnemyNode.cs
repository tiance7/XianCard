using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敌人节点
/// </summary>
public class EnemyNode : MapNodeBase
{
    public uint tplId;   //敌人ID todo 改成列表或者根据需求重构以支持多个怪物初始化

    public EnemyNode(uint tplId, float posX, float posY) : base(posX, posY)
    {
        this.tplId = tplId;
    }
}
