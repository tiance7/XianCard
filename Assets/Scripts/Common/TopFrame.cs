using System;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;

namespace UI.Common
{
    public partial class TopFrame
    {
        //model
        private List<RelicBase> _lstRelic;

        public override void ConstructFromResource()
        {
            base.ConstructFromResource();
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
            RefreshHp();
            RefreshGold();
            InitRelic();
        }

        private void RefreshHp()
        {
            var selfData = BattleModel.Inst.selfData;
            txtHp.text = string.Format("{0}/{1}", selfData.curHp, selfData.maxHp);
        }

        private void RefreshGold()
        {
            txtGold.text = CharModel.Inst.gold.ToString();
        }

        private void InitRelic()
        {
            lstRelic.itemRenderer = OnRelicRender;
            RefreshRelic();
        }

        private void RefreshRelic()
        {
            _lstRelic = CharModel.Inst.GetRelicList();
            lstRelic.numItems = _lstRelic.Count;
        }

        private void OnRelicRender(int index, GObject item)
        {
            RelicRender render = item as RelicRender;
            render.SetData(_lstRelic[index]); 
        }

        private void InitControl()
        {
            BattleModel.Inst.AddListener(BattleEvent.SELF_HP_UPDATE, OnHpUpdate);

            CharModel.Inst.AddListener(CharEvent.GOLD_CHANGE, OnGoldChange);
        }

        private void ReleaseControl()
        {
            BattleModel.Inst.RemoveListener(BattleEvent.SELF_HP_UPDATE, OnHpUpdate);

            CharModel.Inst.RemoveListener(CharEvent.GOLD_CHANGE, OnGoldChange);
        }

        private void OnHpUpdate(object obj)
        {
            RefreshHp();
        }

        private void OnGoldChange(object obj)
        {
            RefreshGold();
        }
    }
}