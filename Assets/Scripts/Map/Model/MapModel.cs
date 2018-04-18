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
    public const int NODE_NUM = 50; //每个地图5个节点 10张随机地图
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
        InitMapNode();
        InitBlock();
    }

    //初始化地图节点
    private void InitMapNode()
    {
        List<MapNodeType> lstNodeType = new List<MapNodeType>() { MapNodeType.NORMAL_ENEMY };    //第一个节点是普通怪
        lstNodeType.AddRange(GetRandomNodeTypeList(4, new List<MapNodeType>() { MapNodeType.NORMAL_ENEMY, MapNodeType.ADVANTURE }));  //生成第2-5个节点

        //生成第6-50个节点 todo 处理限制
        List<MapNodeType> lstAfterNodeType = GetRandomNodeTypeList(NODE_NUM - 5,
            new List<MapNodeType>() { MapNodeType.NORMAL_ENEMY, MapNodeType.ADVANTURE, MapNodeType.BOX, MapNodeType.ELITE, MapNodeType.SHOP });
        lstNodeType.AddRange(lstAfterNodeType);

        List<MapNodeBase> lstMapNode = new List<MapNodeBase>();
        //todo 加入不同的节点类型
        for (int i = 0; i < NODE_NUM; i++)
        {
            //todo 随机节点类型
            MapNodeType nodeType = lstNodeType[i];
            lstMapNode.Add(GetMapNode(nodeType));
            //lstMapNode.Add(new NormalEnemyNode(1/*, 700, BOSS_HEIGHT + i * SINGLE_HEIGHT + UnityEngine.Random.Range(0, SINGLE_HEIGHT)*/));   //todo 随机敌人ID
        }
        _lstOfLstMapNode.Add(lstMapNode);
    }

    //获取随机节点
    private List<MapNodeType> GetRandomNodeTypeList(int nodeNum, List<MapNodeType> lstAvailableType)
    {
        List<MapNodeType> lstNode = new List<MapNodeType>();
        for (int i = 0; i < nodeNum; i++)
        {
            lstNode.Add(GetRandomNodeType(lstAvailableType));
        }
        return lstNode;
    }

    //获得一个随机节点类型
    private MapNodeType GetRandomNodeType(List<MapNodeType> lstNodeType)
    {
        int index = UnityEngine.Random.Range(0, lstNodeType.Count);
        return lstNodeType[index];
    }

    //获取地图节点
    private MapNodeBase GetMapNode(MapNodeType nodeType)
    {
        switch (nodeType)
        {
            case MapNodeType.NORMAL_ENEMY:
                return new NormalEnemyNode(1);  //todo 随机普通怪ID
            case MapNodeType.ELITE:
                return new EliteNode(1);  //todo 随机精英怪ID
            case MapNodeType.ADVANTURE:
                return new AdvantureNode();
            case MapNodeType.SHOP:
                return new ShopNode();
            case MapNodeType.BOX:
                return new BoxNode();
            default:
                Debug.LogError("unhandle map node:" + nodeType);
                break;
        }
        return null;
    }

    //初始化地图列表
    private void InitBlock()
    {
        _lstBlock = new List<Type>
        {
            typeof(MapBlock1),typeof(MapBlock2)
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
