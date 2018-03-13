using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Battle
{
    public partial class UsedCardCom
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
            RefreshUsedCard();
        }

        private void RefreshUsedCard()
        {
            txtUsedNum.text = BattleModel.Inst.GetUsedList().Count.ToString();
        }

        private void InitControl()
        {
            BattleModel.Inst.AddListener(BattleEvent.USED_NUM_UPDATE, OnUsedNumUpdate);
        }

        private void ReleaseControl()
        {
            BattleModel.Inst.RemoveListener(BattleEvent.USED_NUM_UPDATE, OnUsedNumUpdate);
        }

        private void OnUsedNumUpdate(object obj)
        {
            RefreshUsedCard();
        }
    }
}