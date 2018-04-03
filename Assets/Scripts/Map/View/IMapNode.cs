using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 地图节点显示组件接口
/// </summary>
public interface IMapNode
{
    void SetNode(MapNodeBase mapNode);
    MapNodeBase GetNode();
}
