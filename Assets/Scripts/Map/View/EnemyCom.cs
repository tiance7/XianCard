using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Map
{
    public partial class EnemyCom : IMapNode
    {
        private NormalEnemyNode _enemyNode;

        public MapNodeBase GetNode()
        {
            return _enemyNode;
        }

        public void SetNode(MapNodeBase mapNode)
        {
            _enemyNode = mapNode as NormalEnemyNode;
        }
    }
}