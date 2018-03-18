using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTool
{
    public static AnimationCurve hpCurve;

    /// <summary>
    /// 是否还能抽卡
    /// </summary>
    /// <returns></returns>
    public static bool IsDeckHasCard()
    {
        return BattleModel.Inst.GetDeckList().Count > 0;
    }

    /// <summary>
    /// 从弃牌堆还原并洗牌
    /// </summary>
    /// <returns></returns>
    internal static float ShuffleDeckFromUsed()
    {
        BattleModel.Inst.ShuffleDeckFromUsed();
        return 1.0f;    //todo 动画播放时间
    }

    /// <summary>
    /// 是否全部的敌人都死亡
    /// </summary>
    /// <returns></returns>
    internal static bool IsAllEnemyDead()
    {
        foreach (var kv in BattleModel.Inst.GetEnemys())
        {
            if (kv.Value.curHp > 0)
                return false;
        }
        return true;
    }
}
