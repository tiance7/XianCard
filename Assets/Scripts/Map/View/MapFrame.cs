using FairyGUI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

namespace UI.Map
{
    public partial class MapFrame
    {
        private List<GComponent> _lstMapNodeCom = new List<GComponent>();
        private Dictionary<string, MapNodeCom> _dicMapNode = new Dictionary<string, MapNodeCom>();

        public override void ConstructFromResource()
        {
            base.ConstructFromResource();
            MapModel.Inst.Init();   //todo 改成选完职业后初始化
            InitView();
            InitControl();
        }

        private void InitView()
        {
            //todo 随机生成多条路径
            //var lstNode = MapModel.Inst.GetCurrentLayerMapNodes();
            //for (int i = 0; i < MapModel.NODE_NUM; i++)
            //{
            //    var node = lstNode[i];
            //    IMapNode comNode = GetNodeCom(node);
            //    if (comNode == null)
            //        continue;
            //    comNode.SetNode(node);
            //    GComponent gcNode = comNode as GComponent;
            //    gcNode.SetXY(node.posX, node.posY);
            //    comMap.AddChild(gcNode);
            //    _lstMapNodeCom.Add(gcNode);
            //}

            //随机生成地图块
            GenerateRandomBlock();

            //todo 划线连接
        }

        private void GenerateRandomBlock()
        {
            var lstBlock = MapModel.Inst.GetBlocks();
            int index = UnityEngine.Random.Range(0, lstBlock.Count);
            Type typeBlock = lstBlock[index];
            var method = typeBlock.GetMethod("CreateInstance", BindingFlags.Public | BindingFlags.Static);
            GComponent mapBlock = method.Invoke(null, null) as GComponent;
            AddChildAt(mapBlock, 1);    //要在TopFrame下面
            //todo 根据数据初始化不同的地图块
            string data = mapBlock.packageItem.componentData.GetAttribute("customData");
            var lstRoad = data.Split('\r');
            var lstMapNodes = MapModel.Inst.GetCurrentLayerMapNodes();
            foreach (var strRoad in lstRoad)
            {
                var lstPointIndex = strRoad.Split(',');
                foreach (string pointIndex in lstPointIndex)
                {
                    if (_dicMapNode.ContainsKey(pointIndex))
                        continue;
                    int iPointIndex;
                    int.TryParse(pointIndex, out iPointIndex);
                    //todo 这里parse点索引的方式不正确，还需要再修改地图数据的生成方式
                    MapNodeBase nodeBase = lstMapNodes[iPointIndex];
                    MapNodeCom mapNodeCom = mapBlock.GetChild("node" + pointIndex) as MapNodeCom;
                    mapNodeCom.SetNodeData(nodeBase);
                    mapNodeCom.onClick.Add(OnNodeClick);
                    _dicMapNode.Add(pointIndex, mapNodeCom);
                }
            }
        }

        private void OnNodeClick(EventContext context)
        {
            MapNodeCom nodeCom = context.sender as MapNodeCom;
            MapModel.Inst.SetEnterNode(nodeCom.nodeData);
            switch(nodeCom.nodeData.nodeType)
            {
                case MapNodeType.NORMAL_ENEMY:
                    SceneManager.LoadScene(SceneName.BATTLE);
                    break;
                default:
                    Debug.LogError("unhandle node type:" + nodeCom.nodeData.nodeType);
                    break;
            }
        }

        //根据节点类型创建不同的显示对象
        private IMapNode GetNodeCom(MapNodeBase node)
        {
            if (node is NormalEnemyNode)
                return EnemyCom.CreateInstance();
            Debug.LogError("unhandle node:" + node.GetType());
            return null;
        }

        private void InitControl()
        {
            //foreach (var nodeCom in _lstMapNodeCom)
            //{
            //    nodeCom.onClick.Add(OnMapNodeClick);
            //}
        }

        //private void OnMapNodeClick(EventContext context)
        //{
        //    //todo 不同节点类型不同处理方式
        //    IMapNode mapNode = context.sender as IMapNode;
        //    var clickNode = mapNode.GetNode();
        //    MapModel.Inst.SetEnterNode(clickNode);
        //    if (clickNode is EnemyNode)
        //        SceneManager.LoadScene(SceneName.BATTLE);
        //    else
        //        Debug.LogError("unhandle click node:" + clickNode.GetType());
        //}
    }
}