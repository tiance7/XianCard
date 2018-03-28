using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Battle
{
    public partial class TipRender
    {
        internal void SetData(TipStruct tipStruct)
        {
            txtName.text = tipStruct.name;
            imgIcon.url = tipStruct.iconUrl;
            txtDesc.text = tipStruct.desc;
        }
    }
}