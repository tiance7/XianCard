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
        private Dictionary<string, MapNodeCom> _dicNodeCom = new Dictionary<string, MapNodeCom>();

        public override void ConstructFromResource()
        {
            base.ConstructFromResource();
            MapModel.Inst.Init();   //todo 改成选完职业后初始化
            InitView();
            InitControl();
        }

        public override void Dispose()
        {
            ReleaseControl();
            base.Dispose();
        }

        private void InitView()
        {
            //随机生成地图块
            GenerateRandomBlock();
            UpdateCanEnterNode();
        }

        private void GenerateRandomBlock()
        {
            MapModel mapModel = MapModel.Inst;
            GComponent mapBlock = mapModel.GetCurrentBlockCom();
            AddChildAt(mapBlock, 1);    //要在TopFrame下面
            
            //根据数据初始化不同的地图块
            foreach (var child in mapBlock.GetChildren())
            {
                MapNodeCom childNode = child as MapNodeCom;
                if (childNode == null)
                    continue;
                string strIndex = childNode.name.Replace("node", "");
                MapNodeBase nodeData = mapModel.GetCurrentLayerMapNodeData(strIndex);
                if(nodeData == null)
                {
                    Debug.LogError("no node data:" + strIndex);
                    continue;
                }
                childNode.SetNodeData(nodeData);
                childNode.onClick.Add(OnNodeClick);

                _dicNodeCom.Add(strIndex, childNode);
            }
        }

        //根据索引获取节点控件
        private MapNodeCom GetNodeCom(int nodeIndex)
        {
            string strIndex = nodeIndex.ToString();
            if (_dicNodeCom.ContainsKey(strIndex))
                return _dicNodeCom[strIndex];
            return null;
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
        private IMapNode CreateNodeCom(MapNodeBase node)
        {
            if (node is NormalEnemyNode)
                return EnemyCom.CreateInstance();
            Debug.LogError("unhandle node:" + node.GetType());
            return null;
        }

        //更新可进入的节点
        private void UpdateCanEnterNode()
        {
            MapNodeBase lastEnterNode = MapModel.Inst.enterNode;
            List<int> lstCanEnterIndex;
            if (lastEnterNode == null)   //如果还没有进入过节点
                lstCanEnterIndex = new List<int>() { 1 };
            else
                lstCanEnterIndex = GetNextCanEnterIndexList(lastEnterNode.nodeIndex);

            SetAllNodeNotEnter();
            foreach (int canEnterIndex in lstCanEnterIndex)
            {
                MapNodeCom nodeCom = GetNodeCom(canEnterIndex);
                if (nodeCom == null)
                {
                    Debug.LogError("can't find note:" + canEnterIndex);
                    continue;
                }
                nodeCom.SetCanEnter(true);
            }
        }

        //获取可进入的下个节点的列表
        private List<int> GetNextCanEnterIndexList(int nodeIndex)
        {
            List<int> lstNextEnterIndex = new List<int>();
            foreach (List<int> lstBlockRoad in MapModel.Inst.lstOfLstBlockRoad)
            {
                int roadLength = lstBlockRoad.Count;
                for (int i = 0; i < roadLength - 1; i++)
                {
                    if (lstBlockRoad[i] != nodeIndex)
                        continue;
                    lstNextEnterIndex.Add(nodeIndex);
                    break;
                }
            }
            return lstNextEnterIndex;
        }

        //设置所有节点为不可进入状态
        private void SetAllNodeNotEnter()
        {
            GComponent mapBlock = MapModel.Inst.GetCurrentBlockCom();
            foreach (var child in mapBlock.GetChildren())
            {
                MapNodeCom childNode = child as MapNodeCom;
                if (childNode != null)
                    childNode.SetCanEnter(false);
            }
        }


        private void InitControl()
        {
            MapModel.Inst.AddListener(MapEvent.ENTER_NODE_UPDATE, OnEnterNodeUpdate);
        }

        private void ReleaseControl()
        {
            MapModel.Inst.RemoveListener(MapEvent.ENTER_NODE_UPDATE, OnEnterNodeUpdate);
        }

        private void OnEnterNodeUpdate(object obj)
        {
            UpdateCanEnterNode();
        }
    }
}