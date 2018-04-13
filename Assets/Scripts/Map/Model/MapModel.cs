using System;
using System.Collections;
using System.Collections.Generic;
using UI.Map;
using UnityEngine;

/// <summary>
/// 大地图数据层
/// </summary>
public class MapModel : ModelBase
{
    #region
    private readonly static MapModel _inst = new MapModel();
    static MapModel() { }
    public static MapModel Inst { get { return _inst; } }
    #endregion

    //define
    public const int NODE_NUM = 16; //每层地图除了BOSS以外的节点数量
    private const int SINGLE_HEIGHT = 120; //单个对象占据的高度
    private const int BOSS_HEIGHT = 500; //BOSS占据的高度

    public MapNodeBase enterNode { get; private set; }  //当前进入的节点

    private List<List<MapNodeBase>> _lstOfLstMapNode = new List<List<MapNodeBase>>();
    private int _currentLayerIndex = 0; //当前地图层次的索引

    private List<Type> _lstBlock;

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        //todo 根据职业初始化
        List<MapNodeBase> lstMapNode = new List<MapNodeBase>();
        //todo 加入不同的节点类型
        for (int i = 0; i < NODE_NUM; i++)
        {
            lstMapNode.Add(new EnemyNode(1, 700, BOSS_HEIGHT + i * SINGLE_HEIGHT + UnityEngine.Random.Range(0, SINGLE_HEIGHT)));   //todo 随机敌人ID 随机x坐标
        }
        _lstOfLstMapNode.Add(lstMapNode);

        InitBlock();
    }

    private void InitBlock()
    {
        _lstBlock = new List<Type>
        {
            typeof(MapBlock1)
        };
    }

    public List<Type> GetBlocks()
    {
        return _lstBlock;
    }

    /// <summary>
    /// 获取当前层次的地图节点列表
    /// </summary>
    /// <param name="layerIndex"></param>
    /// <returns></returns>
    public List<MapNodeBase> GetCurrentLayerMapNodes()
    {
        return _lstOfLstMapNode[_currentLayerIndex];
    }

    /// <summary>
    /// 设置进入的节点
    /// </summary>
    /// <param name="enterNode"></param>
    internal void SetEnterNode(MapNodeBase enterNode)
    {
        this.enterNode = enterNode;
    }
}
