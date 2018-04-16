using System;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;

namespace UI.Common
{
    public partial class RelicRender
    {
        //model
        private RelicBase _relicBase;
        private RelicTemplate _template;

        //view
        private RelicTip _relicTip;

        public override void ConstructFromResource()
        {
            base.ConstructFromResource();
            InitControl();
        }

        public override void Dispose()
        {
            ReleaseControl();
            ReleaseTip();
            base.Dispose();
        }

        internal void SetData(RelicBase relicBase)
        {
            _relicBase = relicBase;
            _template = RelicTemplateData.GetData(relicBase.tplId);
            if (_template == null)
                return;
            imgRelic.url = ResPath.GetUiImagePath(PackageName.RELIC, _template.szImg);
        }

        private void InitControl()
        {
            this.onRollOver.Add(OnRollOver);
            this.onRollOut.Add(OnRollOut);
        }

        private void ReleaseControl()
        {
            this.onRollOver.Remove(OnRollOver);
            this.onRollOut.Remove(OnRollOut);
        }

        private void OnRollOver(EventContext context)
        {
            ShowTip();
        }

        private void OnRollOut(EventContext context)
        {
            HideTip();
        }

        private void ShowTip()
        {
            if (_relicTip == null)
            {
                _relicTip = RelicTip.CreateInstance();
                _relicTip.touchable = false;
                _relicTip.txtName.text = _template.szName;
                _relicTip.txtDesc.text = _template.szDesc;
                _relicTip.y = this.height;
                AddChild(_relicTip);
            }
            _relicTip.visible = true;
        }

        private void ReleaseTip()
        {
            if (_relicTip != null)
                _relicTip.visible = false;
        }

        private void HideTip()
        {
            if (_relicTip != null)
            {
                _relicTip.Dispose();
                _relicTip = null;
            }
        }
    }
}