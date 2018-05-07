using FairyGUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
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
    private const int BLOCK_NUM = 10; //10张随机地图
    private const int STEP_PER_BLOCK = 5; //每张地图可以行走的步数
    private const int SINGLE_HEIGHT = 120; //单个对象占据的高度
    private const int BOSS_HEIGHT = 500; //BOSS占据的高度

    public MapNodeBase enterNode { get; private set; }  //当前进入的节点

    //private List<List<MapNodeBase>> _lstOfLstMapNode = new List<List<MapNodeBase>>();
    private int _currentLayerIndex = 0; //当前地图层次的索引

    private List<Type> _lstBlockType;
    private List<GComponent> _lstBlockCom;
    public List<List<int>> lstOfLstBlockRoad = new List<List<int>>();   //地图块的路线列表
    private Dictionary<string, MapNodeBase> _dicMapNode = new Dictionary<string, MapNodeBase>();
    private int _leftBoxNum;    //剩余可生成节点的数量
    private int _leftShopNum;
    private int _leftElitNum;
    private int _lastElitStep;  //最后一个生成节点的位置
    private int _lastShopStep;
    private int _lastBoxStep;

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        InitBlock();
        //InitMapNode();
    }

    //初始化地图列表
    private void InitBlock()
    {
        //init model
        _lstBlockType = new List<Type>
        {
            typeof(MapBlock1),typeof(MapBlock2)
        };
        _leftBoxNum = 6;
        _leftShopNum = 6;
        _leftElitNum = 12;
        lstOfLstBlockRoad.Clear();

        //生成具体数据
        _lstBlockCom = new List<GComponent>();
        for (int i = 0; i < BLOCK_NUM; i++)
        {
            //生成block组件
            int index = UnityEngine.Random.Range(0, _lstBlockType.Count);
            Type typeBlock = _lstBlockType[index];
            var method = typeBlock.GetMethod("CreateInstance", BindingFlags.Public | BindingFlags.Static);
            GComponent mapBlock = method.Invoke(null, null) as GComponent;
            _lstBlockCom.Add(mapBlock);

            //生成可行走的节点的类型
            string data = mapBlock.packageItem.componentData.GetAttribute("customData");
            string[] lstRoad = data.Split('\r');
            foreach (string strRoad in lstRoad)
            {
                string[] lstPointIndex = strRoad.Split(',');
                int roadCount = lstPointIndex.Length;
                List<int> lstBlockRoad = new List<int>();
                for (int blockStep = 0; blockStep < roadCount; blockStep++)
                {
                    string strPointIndex = lstPointIndex[blockStep];
                    string nodeKey = GetNodeKey(i, strPointIndex);
                    if (_dicMapNode.ContainsKey(nodeKey))
                        continue;
                    int step = i * STEP_PER_BLOCK + blockStep + 1;
                    int nodeIndex;
                    int.TryParse(strPointIndex, out nodeIndex);
                    lstBlockRoad.Add(nodeIndex);
                    MapNodeBase nodeBase = GetRandomNode(step, nodeIndex);
                    _dicMapNode.Add(nodeKey, nodeBase);
                }
                lstOfLstBlockRoad.Add(lstBlockRoad);
            }
        }
    }

    /// <summary>
    /// 获取当前层地图的节点数据
    /// </summary>
    /// <param name="pointIndex"></param>
    /// <returns></returns>
    public MapNodeBase GetCurrentLayerMapNodeData(string pointIndex)
    {
        string nodeKey = GetNodeKey(_currentLayerIndex, pointIndex);
        if (_dicMapNode.ContainsKey(nodeKey))
            return _dicMapNode[nodeKey];
        return null;
    }

    private string GetNodeKey(int layerIndex, string pointIndex)
    {
        return string.Format("{0}_{1}", layerIndex, pointIndex);
    }

    //根据步数随机生成节点
    private MapNodeBase GetRandomNode(int step, int nodeIndex)
    {
        if (step == 1)  //第一个节点是普通怪
            return GetMapNode(MapNodeType.NORMAL_ENEMY, nodeIndex);

        List<MapNodeType> lstNodeType;
        if (step <= 5)  //前5个节点只会是普通怪或者奇遇
        {
            lstNodeType = new List<MapNodeType>() { MapNodeType.NORMAL_ENEMY, MapNodeType.ADVANTURE };
        }
        else
        {
            lstNodeType = new List<MapNodeType>() { MapNodeType.NORMAL_ENEMY, MapNodeType.ADVANTURE };
            TryAddNodeType(lstNodeType, MapNodeType.BOX, step);
            TryAddNodeType(lstNodeType, MapNodeType.ELITE, step);
            TryAddNodeType(lstNodeType, MapNodeType.SHOP, step);
        }

        MapNodeType nodeType = GetRandomNodeType(lstNodeType);
        return GetMapNode(nodeType, nodeIndex);
    }

    //尝试加入节点类型
    private void TryAddNodeType(List<MapNodeType> lstNodeType, MapNodeType nodeType, int step)
    {
        switch (nodeType)
        {
            case MapNodeType.NONE:
            case MapNodeType.NORMAL_ENEMY:
            case MapNodeType.ADVANTURE:
                //do nothing
                break;
            case MapNodeType.ELITE:
                if (_leftElitNum <= 0 && step - _lastElitStep >= 4)
                    return;
                --_leftElitNum;
                _lastElitStep = step;
                lstNodeType.Add(MapNodeType.ELITE);
                break;
            case MapNodeType.SHOP:
                if (_leftShopNum <= 0 && step - _lastShopStep >= 7)
                    return;
                --_leftShopNum;
                _lastShopStep = step;
                lstNodeType.Add(MapNodeType.SHOP);
                break;
            case MapNodeType.BOX:
                if (_leftBoxNum <= 0 && step - _lastBoxStep >= 7)
                    return;
                --_leftBoxNum;
                _lastBoxStep = step;
                lstNodeType.Add(MapNodeType.BOX);
                break;
            default:
                Debug.LogError("unhandle add node type:" + nodeType);
                break;
        }
    }

    /// <summary>
    /// 获取地图块组件
    /// </summary>
    /// <returns></returns>
    public GComponent GetCurrentBlockCom()
    {
        return _lstBlockCom[_currentLayerIndex];
    }

    //初始化地图节点
    //private void InitMapNode()
    //{
    //    List<MapNodeType> lstNodeType = new List<MapNodeType>() { MapNodeType.NORMAL_ENEMY };    //第一个节点是普通怪
    //    lstNodeType.AddRange(GetRandomNodeTypeList(4, new List<MapNodeType>() { MapNodeType.NORMAL_ENEMY, MapNodeType.ADVANTURE }));  //生成第2-5个节点

    //    //生成第6-50个节点 todo 处理限制
    //    List<MapNodeType> lstAfterNodeType = GetRandomNodeTypeList(NODE_NUM - 5,
    //        new List<MapNodeType>() { MapNodeType.NORMAL_ENEMY, MapNodeType.ADVANTURE, MapNodeType.BOX, MapNodeType.ELITE, MapNodeType.SHOP });
    //    lstNodeType.AddRange(lstAfterNodeType);

    //    List<MapNodeBase> lstMapNode = new List<MapNodeBase>();
    //    //todo 加入不同的节点类型
    //    for (int i = 0; i < NODE_NUM; i++)
    //    {
    //        //todo 随机节点类型
    //        MapNodeType nodeType = lstNodeType[i];
    //        lstMapNode.Add(GetMapNode(nodeType));
    //        //lstMapNode.Add(new NormalEnemyNode(1/*, 700, BOSS_HEIGHT + i * SINGLE_HEIGHT + UnityEngine.Random.Range(0, SINGLE_HEIGHT)*/));   //todo 随机敌人ID
    //    }
    //    _lstOfLstMapNode.Add(lstMapNode);
    //}

    //获取随机节点
    //private List<MapNodeType> GetRandomNodeTypeList(int nodeNum, List<MapNodeType> lstAvailableType)
    //{
    //    List<MapNodeType> lstNode = new List<MapNodeType>();
    //    for (int i = 0; i < nodeNum; i++)
    //    {
    //        lstNode.Add(GetRandomNodeType(lstAvailableType));
    //    }
    //    return lstNode;
    //}

    //获得一个随机节点类型
    private MapNodeType GetRandomNodeType(List<MapNodeType> lstNodeType)
    {
        int index = UnityEngine.Random.Range(0, lstNodeType.Count);
        return lstNodeType[index];
    }

    //获取地图节点
    private MapNodeBase GetMapNode(MapNodeType nodeType, int nodeIndex)
    {
        switch (nodeType)
        {
            case MapNodeType.NORMAL_ENEMY:
                return new NormalEnemyNode(nodeIndex, 1);  //todo 随机普通怪ID
            case MapNodeType.ELITE:
                return new EliteNode(nodeIndex, 1);  //todo 随机精英怪ID
            case MapNodeType.ADVANTURE:
                return new AdvantureNode(nodeIndex);
            case MapNodeType.SHOP:
                return new ShopNode(nodeIndex);
            case MapNodeType.BOX:
                return new BoxNode(nodeIndex);
            default:
                Debug.LogError("unhandle map node:" + nodeType);
                break;
        }
        return null;
    }

    /// <summary>
    /// 设置进入的节点
    /// </summary>
    /// <param name="enterNode"></param>
    internal void SetEnterNode(MapNodeBase enterNode)
    {
        this.enterNode = enterNode;
        SendEvent(MapEvent.ENTER_NODE_UPDATE);
    }
}
