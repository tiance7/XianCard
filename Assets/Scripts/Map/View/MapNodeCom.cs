using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Map
{
    public partial class MapNodeCom
    {
        public MapNodeBase nodeData { get; private set; }

        internal void SetNodeData(MapNodeBase nodeData)
        {
            this.nodeData = nodeData;
            InitView();
        }

        private void InitView()
        {
            TypeControl typeControl;
            switch (nodeData.nodeType)
            {
                case MapNodeType.NORMAL_ENEMY:
                    typeControl = TypeControl.NORMAL_ENEMY;
                    break;
                case MapNodeType.ELITE:
                    typeControl = TypeControl.ELITE;
                    break;
                case MapNodeType.ADVANTURE:
                    typeControl = TypeControl.ADVANTURE;
                    break;
                case MapNodeType.SHOP:
                    typeControl = TypeControl.SHOP;
                    break;
                case MapNodeType.BOX:
                    typeControl = TypeControl.BOX;
                    break;
                default:
                    Debug.LogError("unhandle node type:" + nodeData.nodeType);
                    typeControl = TypeControl.UNKNOW;
                    break;
            }
            cType.SetSelectedIndex((int)typeControl);
        }
    }

    enum TypeControl
    {
        NORMAL_ENEMY,
        ADVANTURE,
        UNKNOW,
        SHOP,
        BOX,
        EXIT,
        ELITE
    }
}