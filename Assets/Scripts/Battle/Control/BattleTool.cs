using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTool
{
    public static AnimationCurve hpCurve;
    private static int _objInstId = 0;  //对象实例ID

    /// <summary>
    /// 是否还能抽卡
    /// </summary>
    /// <returns></returns>
    public static bool IsDeckHasCard()
    {
        return BattleModel.Inst.GetDeckList().Count > 0;
    }

    internal static int GetObjectInstId()
    {
        ++_objInstId;
        return _objInstId;
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

    /// <summary>
    /// 获取回合行动
    /// </summary>
    /// <param name="fighterInstId"></param>
    /// <returns></returns>
    internal static BoutAction GetBoutAction(int fighterInstId)
    {
        var enmeyInst = BattleModel.Inst.GetEnemy(fighterInstId);
        if (enmeyInst == null)
            return null;
        return enmeyInst.boutAction;
    }

    /// <summary>
    /// 获取回合行为提示
    /// </summary>
    /// <param name="boutAction"></param>
    /// <returns></returns>
    internal static TipStruct GetActionTip(BoutAction boutAction)
    {
        switch (boutAction.enemyAction)
        {
            case EnemyAction.ATTACK:
                var attackTip = new TipStruct()
                {
                    name = GameText.ACTION_ATTACK,
                    iconUrl = ResPath.GetUiImagePath(PackageName.BATTLE, "sword_icon"),
                    desc = string.Format(GameText.BATTLE_5, boutAction.iValue)
                };
                return attackTip;
            default:
                Debug.LogError("unhandle enemy action type:" + boutAction.enemyAction);
                break;
        }
        return null;
    }

    /// <summary>
    /// 获取战斗者的BUFF实例列表
    /// </summary>
    /// <param name="fighterInstId"></param>
    /// <returns></returns>
    internal static List<BuffInst> GetFighterBuff(int fighterInstId)
    {
        BattleModel battleModel = BattleModel.Inst;
        if (fighterInstId == battleModel.selfData.instId)
            return battleModel.selfData.lstBuffInst;

        EnemyInstance enemy = battleModel.GetEnemy(fighterInstId);
        if (enemy == null)
        {
            Debug.LogError("can't find fighter:" + fighterInstId);
            return null;
        }
        return enemy.lstBuffInst;
    }

    /// <summary>
    /// 获取BUFF提示
    /// </summary>
    /// <param name="buffInst"></param>
    /// <returns></returns>
    internal static TipStruct GetBuffTip(BuffInst buffInst)
    {
        BuffTemplate template = BuffTemplateData.GetData(buffInst.tplId);
        if (template == null)
            return null;
        TipStruct tipStruct = new TipStruct()
        {
            name = template.szName,
            desc = template.szDesc,
            iconUrl = ResPath.GetUiImagePath(PackageName.BATTLE, template.szImg)
        };
        return tipStruct;
    }

    internal static int AdjustAttackVal(ObjectBase caster, ObjectBase target, int iVal)
    {
        // 如果目标身上有易伤，增加伤害
        if (target != null)
        {
            BuffInst vulnerableBuff = target.GetBuffInstByType(BuffType.VULNERABLE);
            if (vulnerableBuff != null)
            {
                iVal = (iVal * (100 + vulnerableBuff.effectVal) / 100);
            }
        }

        // 攻击者身上有虚弱BUFF，减少伤害
        if (caster != null)
        {
            BuffInst weakBuff = caster.GetBuffInstByType(BuffType.WEAK);
            if (weakBuff != null)
            {
                iVal = (iVal * (100 - weakBuff.effectVal) / 100);
                if (iVal < 0)
                {
                    iVal = 0;
                }
            }
        }

        return iVal;
    }
}