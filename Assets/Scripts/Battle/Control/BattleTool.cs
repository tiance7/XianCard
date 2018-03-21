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

    /// <summary>
    /// 生成备选奖励卡牌
    /// </summary>
    /// <returns></returns>
    public static List<CardTemplate> GenerateRewardCards()
    {
        var selfJob = BattleModel.Inst.job;
        List<CardTemplate> lstReward = new List<CardTemplate>();
        foreach (var kv in CardTemplateData.Data)
        {
            if (kv.Value.nJob != selfJob || kv.Value.nUpgradeId == 0)   //如果职业不符合 或者 是不可升级的卡牌
                continue;
            lstReward.Add(kv.Value);
        }

        //随机排序
        System.Random random = new System.Random();
        lstReward.Sort(delegate (CardTemplate a, CardTemplate b)
        {
            return random.Next(-1, 2);
        });

        return lstReward.GetRange(0, 3);    //todo 如果有法宝，根据法宝数量修正奖励卡牌的备选数量
    }

}
