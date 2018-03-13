using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Battle
{
    /// <summary>
    /// 抽牌堆界面
    /// </summary>
    public partial class CardDeckCom
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
            RefreshDeckNum();
        }

        private void RefreshDeckNum()
        {
            txtDeckNum.text = BattleModel.Inst.GetDeckList().Count.ToString();
        }

        private void InitControl()
        {
            BattleModel.Inst.AddListener(BattleEvent.DECK_NUM_UPDATE, OnDeckNumUpdate);
        }

        private void ReleaseControl()
        {
            BattleModel.Inst.RemoveListener(BattleEvent.DECK_NUM_UPDATE, OnDeckNumUpdate);
        }

        private void OnDeckNumUpdate(object obj)
        {
            RefreshDeckNum();
        }
    }
}