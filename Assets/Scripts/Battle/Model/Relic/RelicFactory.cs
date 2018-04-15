using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicFactory
{
    public static RelicBase CreateNewRelic(uint iRelicId)
    {
        switch (iRelicId)
        {
            case RelicId.HUZANG_HUFU:
                return new RelicHuZangHuFu();
            case RelicId.WUHUOQIQIN_SHAN:
                return new RelicWuHuoQiQinShan();
            case RelicId.WUJIN_HU:
                return new RelicWuJinHu();
            case RelicId.XUEJING_SHI:
                return new RelicXueJingShi();
            default:
                //Debug.LogError("unknown iRelicId:" + iRelicId);
                return null;
        }
    }
}