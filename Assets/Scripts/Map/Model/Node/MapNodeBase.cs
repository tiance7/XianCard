using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 地图节点数据基类
/// </summary>
public class MapNodeBase
{
    public float posX { get; private set; }    //坐标
    public float posY { get; private set; }    //坐标
    public bool isPass = false; //是否通过

    public MapNodeBase(float posX, float posY)
    {
        this.posX = posX;
        this.posY = posY;
    }
}
