using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Common
{
    public partial class TopFrame
    {
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