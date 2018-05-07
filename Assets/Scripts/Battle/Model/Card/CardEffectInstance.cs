using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffectFactory
{
    public static List<CardEffectBase> lstCardEffectBases;
    public static bool bInit = false;

    public static void Init()
    {
        if (bInit)
        {
            return;
        }
        lstCardEffectBases = new List<CardEffectBase>(new CardEffectBase[CardEffectType.CARD_EFFECT_MAX]);
        lstCardEffectBases[(int)CardEffectType.NONE] = new CardEffectBase();
        lstCardEffectBases[(int)CardEffectType.ONE_DAMAGE] = new CardEffectOneDamage();
        lstCardEffectBases[(int)CardEffectType.GET_ARMOR] = new CardEffectGetArmor();
        lstCardEffectBases[(int)CardEffectType.GIVE_BUFF] = new CardEffectGiveBuff();
        lstCardEffectBases[(int)CardEffectType.DRAW_CARD] = new CardEffectDrawCard();
        lstCardEffectBases[(int)CardEffectType.GIVE_COST] = new CardEffectGiveCost();
        lstCardEffectBases[(int)CardEffectType.CONSUME_BUFF_GET_BUFF] = new CardEffectConsumeBuffGetBuff();
        lstCardEffectBases[(int)CardEffectType.ONE_DAMAGE_IGNORE_ARMOR] = lstCardEffectBases[(int)CardEffectType.ONE_DAMAGE];
        lstCardEffectBases[(int)CardEffectType.DAMAGE_SELF] = new CardEffectDamageSelf();
        lstCardEffectBases[(int)CardEffectType.DRAW_CARD_UNTIL_NOTATTACK] = new CardEffectDrawCardUntilNotAttack();

        bInit = true;
    }

    public static CardEffectBase GetCardEffect(uint nType)
    {
        if (nType >= CardEffectType.CARD_EFFECT_MAX)
        {
            return null;
        }

        return lstCardEffectBases[(int)nType];
    }
}


/// <summary>
/// 卡牌效果基类
/// </summary>
public class CardEffectBase
{
    public CardEffectBase()
    {
    }

    public virtual bool CanTriggerCardEffect(CardEffectTemplate effectTplt)
    {
        if (effectTplt == null)
            return false;

        BattleModel battleModel = BattleModel.Inst;
        switch (effectTplt.iEffectTrigType)
        {
            case CardEffectTrigType.NONE:
                break;
            case CardEffectTrigType.GIVE_NOT_BLOCK_DAMAGE:
                if (battleModel.effectStat.damageLife > 0)
                    return true;
                return false;
            case CardEffectTrigType.SELF_HAS_BUFF_TYPE:
                return battleModel.selfData.HasBuff(effectTplt.iTrigVal);
            case CardEffectTrigType.PRE_EFFECT_DRAW_CARD_TYPE_ATTACK:
                List<CardInstance> lstDrawCard = battleModel.effectStat.lstDrawCard;
                if (lstDrawCard.Count > 0 && lstDrawCard[lstDrawCard.Count-1].cardType == CardType.ATTACK)
                {
                    return true;
                }
                return false;
            default:
                Debug.LogError("unhandle card EffectTrigType:" + effectTplt.iEffectTrigType);
                break;
        }

        return true;
    }

    public virtual void DoEffect(BattleManager battlemgr, CardInstance cardInstance, CardEffectTemplate effectTplt, int targetInstId)
    {
        return;
    }
}

/// <summary>
/// 具体的子类效果
/// </summary>
public class CardEffectOneDamage : CardEffectBase
{
    public CardEffectOneDamage() : base(){}

    public override void DoEffect(BattleManager battlemgr, CardInstance cardInstance, CardEffectTemplate effectTplt, int targetInstId)
    {
        BattleModel battleModel = BattleModel.Inst;

        if (effectTplt.nTarget == CardEffectTargetType.ONE_ENEMY)
        {
            int iDmgCount = BattleTool.GetCardEffectCount(effectTplt);
            Core.Inst.StartCoroutine(battlemgr.DamageEnemyCoroutine(iDmgCount, targetInstId, effectTplt));
        }
        else if (effectTplt.nTarget == CardEffectTargetType.ALL_ENEMY)
        {
            int iDmgCount = BattleTool.GetCardEffectCount(effectTplt);
            Core.Inst.StartCoroutine(battlemgr.DamageAllEnemyCoroutine(iDmgCount, effectTplt));
        }
        return;
    }
}

public class CardEffectGetArmor : CardEffectBase
{
    public CardEffectGetArmor() : base() {}

    public override void DoEffect(BattleManager battlemgr, CardInstance cardInstance, CardEffectTemplate effectTplt, int targetInstId)
    {
        BattleModel battleModel = BattleModel.Inst;
        // 如果有脆弱，减少增加的护甲值
        int iAddArmor = effectTplt.iEffectValue;
        iAddArmor = BattleTool.AdjustArmorVal(battleModel.selfData, iAddArmor);

        battleModel.AddArmor(battleModel.selfData, iAddArmor);
        return;
    }
}

public class CardEffectGiveBuff : CardEffectBase
{
    public CardEffectGiveBuff() : base() {}

    public override void DoEffect(BattleManager battlemgr, CardInstance cardInstance, CardEffectTemplate effectTplt, int targetInstId)
    {
        int iCount = BattleTool.GetCardEffectCount(effectTplt);
        BattleModel battleModel = BattleModel.Inst;
        if (effectTplt.nTarget == CardEffectTargetType.ONE_ENEMY)
        {
            battleModel.AddBuff(battleModel.selfData, battleModel.GetEnemy(targetInstId), (uint)effectTplt.iEffectValue, iCount);
        }
        else if (effectTplt.nTarget == CardEffectTargetType.ALL_ENEMY)
        {
            foreach (KeyValuePair<int, EnemyInstance> pair in battleModel.GetEnemys())
            {
                battleModel.AddBuff(battleModel.selfData, pair.Value, (uint)effectTplt.iEffectValue, iCount);
            }
        }
        else if (effectTplt.nTarget == CardEffectTargetType.SELF)
        {
            battleModel.AddBuff(battleModel.selfData, battleModel.selfData, (uint)effectTplt.iEffectValue, iCount);
        }
        return;
    }
}

public class CardEffectDrawCard : CardEffectBase
{
    public CardEffectDrawCard() : base() { }

    public override void DoEffect(BattleManager battlemgr, CardInstance cardInstance, CardEffectTemplate effectTplt, int targetInstId)
    {
        battlemgr.SelfDrawCard(effectTplt.iEffectValue, false);
    }
}

public class CardEffectDrawCardUntilNotAttack : CardEffectBase
{
    public CardEffectDrawCardUntilNotAttack() : base() { }

    public override void DoEffect(BattleManager battlemgr, CardInstance cardInstance, CardEffectTemplate effectTplt, int targetInstId)
    {
        battlemgr.SelfDrawCardUntilNotAttack();
    }
}

public class CardEffectGiveCost : CardEffectBase
{
    public CardEffectGiveCost() : base() { }

    public override void DoEffect(BattleManager battlemgr, CardInstance cardInstance, CardEffectTemplate effectTplt, int targetInstId)
    {
        BattleModel battleModel = BattleModel.Inst;
        battleModel.ReduceCost(-effectTplt.iEffectValue);
        battleModel.effectStat.getCostCount += (uint)effectTplt.iEffectValue;
        battleModel.roundStat.getCostCount += (uint)effectTplt.iEffectValue;
        battleModel.battleStat.getCostCount += (uint)effectTplt.iEffectValue;
    }

}

public class CardEffectConsumeBuffGetBuff : CardEffectBase
{
    public CardEffectConsumeBuffGetBuff() : base() { }

    public override void DoEffect(BattleManager battlemgr, CardInstance cardInstance, CardEffectTemplate effectTplt, int targetInstId)
    {
        BattleModel battleModel = BattleModel.Inst;
        BuffInst rmBuff = battleModel.selfData.GetBuffInst((uint)effectTplt.iEffectValue);
        if (rmBuff != null)
        {
            battleModel.RemoveBuff(battleModel.selfData, rmBuff);
            battleModel.AddBuff(battleModel.selfData, battleModel.selfData, (uint)effectTplt.iEffectValue_2, rmBuff.effectVal);
        }
    }
}

public class CardEffectDamageSelf : CardEffectBase
{
    public CardEffectDamageSelf() : base() { }

    public override void DoEffect(BattleManager battlemgr, CardInstance cardInstance, CardEffectTemplate effectTplt, int targetInstId)
    {
        BattleModel battleModel = BattleModel.Inst;
        battleModel.ReduceSelfHp(effectTplt.iEffectValue);
    }
}