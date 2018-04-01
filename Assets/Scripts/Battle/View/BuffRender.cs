using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Battle
{
    public partial class BuffRender
    {
        internal void SetData(BuffInst buffInst)
        {
            BuffTemplate templet = BuffTemplateData.GetData(buffInst.tplId);
            if (templet == null)
                return;
            img.url = ResPath.GetUiImagePath(PackageName.BATTLE, templet.szImg);
            switch(templet.nType)
            {
                case BuffType.ADD_ARMOR_ROUND_END:
                case BuffType.MULTI_ARMOR:
                    txtValue.text = buffInst.effectVal.ToString();
                    txtValue.visible = true;
                    break;
                case BuffType.MAGIC_SWORD:  //幻剑
                    txtValue.text = buffInst.effectVal.ToString();
                    txtValue.visible = true;
                    break;
                default:
                    txtValue.text = buffInst.leftBout.ToString();
                    txtValue.visible = buffInst.leftBout > 0;
                    break;
            }
        }
    }
}