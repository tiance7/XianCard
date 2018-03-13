using System;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;

namespace UI.Battle
{
    public partial class Fighter
    {
        public int instId { get; private set; }
        private List<BuffInst> _lstBuffInst;

        public override void ConstructFromResource()
        {
            base.ConstructFromResource();
            InitView();
        }

        private void InitView()
        {
            lstBuff.itemRenderer = OnBuffRender;
        }

        private void OnBuffRender(int index, GObject item)
        {
            BuffRender render = item as BuffRender;
            render.SetData(_lstBuffInst[index]);
        }

        /// <summary>
        /// 设置buff实例列表
        /// </summary>
        /// <param name="lstBuffInst"></param>
        public void SetBuffInstList(List<BuffInst> lstBuffInst)
        {
            _lstBuffInst = lstBuffInst;
            lstBuff.numItems = lstBuffInst.Count;
        }

        internal void Init(EnemyInstance enemyInstance)
        {
            instId = enemyInstance.instId;
            rootContainer.touchChildren = false;
            pgsHp.max = pgsHp.value = enemyInstance.maxHp;
            imgAvatar.url = ResPath.GetUiImagePath(PackageName.BATTLE, "guaiwu");   //todo 从敌人模板表读取外观
        }

        public void UpdateHp(int newHp)
        {
            pgsHp.TweenValue(newHp, 0.5f);
        }

        internal void UpdateAction(BoutAction boutAction)
        {
            switch (boutAction.enemyAction)
            {
                case EnemyAction.ATTACK:
                    SetAction(ActionControl.ATTACK);
                    txtAttack.text = boutAction.iValue.ToString();
                    break;
                default:
                    Debug.LogError("unhandle action:" + boutAction.enemyAction);
                    break;
            }
        }

        private void SetAction(ActionControl actionControl)
        {
            ctrlAction.SetSelectedIndex((int)actionControl);
        }

        enum ActionControl
        {
            NONE,
            ATTACK
        }
    }
}