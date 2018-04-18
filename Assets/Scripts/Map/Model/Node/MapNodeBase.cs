using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 地图节点数据基类
/// </summary>
public class MapNodeBase
{
    public bool isPass = false; //是否通过
    public MapNodeType nodeType;

    public MapNodeBase(MapNodeType nodeType)
    {
        this.nodeType = nodeType;
    }
}
