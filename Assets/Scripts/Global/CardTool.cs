using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardTool
{
    private static uint _cardInstIdIndex = 0;  //卡牌实例ID分配索引


    /// <summary>
    /// 获取唯一卡牌实例索引
    /// </summary>
    /// <returns></returns>
    public static uint GetUniCardInstId()
    {
        ++_cardInstIdIndex;
        return _cardInstIdIndex;
    }
}
