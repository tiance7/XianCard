using FairyGUI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Map
{
    public partial class MapFrame
    {
        private const int SINGLE_HEIGHT = 120; //单个对象占据的高度
        private const int BOSS_HEIGHT = 500; //BOSS占据的高度

        private List<MonsterCom> _lstMonsterCom = new List<MonsterCom>();

        public override void ConstructFromResource()
        {
            base.ConstructFromResource();
            InitView();
            InitControl();
        }

        private void InitView()
        {
            //todo 随机生成多条路径
            for (int i = 0; i < 16; i++)
            {
                MonsterCom monsterCom = MonsterCom.CreateInstance();
                monsterCom.x = 700; //todo 随机坐标
                monsterCom.y = BOSS_HEIGHT + i * SINGLE_HEIGHT + Random.Range(0, SINGLE_HEIGHT);
                comMap.AddChild(monsterCom);
                _lstMonsterCom.Add(monsterCom);
            }

            //todo 划线连接
        }

        private void InitControl()
        {
            foreach (var monsterCom in _lstMonsterCom)
            {
                monsterCom.onClick.Add(OnMonsterClick);
            }
        }

        private void OnMonsterClick(EventContext context)
        {
            SceneManager.LoadScene(SceneName.BATTLE);
        }
    }
}