using FairyGUI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace UI.Map
{
    public partial class MapFrame
    {
        private List<GComponent> _lstMapNodeCom = new List<GComponent>();

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
            var lstNode = MapModel.Inst.GetCurrentLayerMapNodes();
            for (int i = 0; i < MapModel.NODE_NUM; i++)
            {
                var node = lstNode[i];
                IMapNode comNode = GetNodeCom(node);
                if (comNode == null)
                    continue;
                comNode.SetNode(node);
                GComponent gcNode = comNode as GComponent;
                gcNode.SetXY(node.posX, node.posY);
                comMap.AddChild(gcNode);
                _lstMapNodeCom.Add(gcNode);
            }

            //todo 划线连接
        }

        //根据节点类型创建不同的显示对象
        private IMapNode GetNodeCom(MapNodeBase node)
        {
            if (node is EnemyNode)
                return EnemyCom.CreateInstance();
            Debug.LogError("unhandle node:" + node.GetType());
            return null;
        }

        private void InitControl()
        {
            foreach (var nodeCom in _lstMapNodeCom)
            {
                nodeCom.onClick.Add(OnMapNodeClick);
            }
        }

        private void OnMapNodeClick(EventContext context)
        {
            //todo 不同节点类型不同处理方式
            IMapNode mapNode = context.sender as IMapNode;
            var clickNode = mapNode.GetNode();
            MapModel.Inst.SetEnterNode(clickNode);
            if (clickNode is EnemyNode)
                SceneManager.LoadScene(SceneName.BATTLE);
            else
                Debug.LogError("unhandle click node:" + clickNode.GetType());
        }
    }
}