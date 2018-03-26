using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleModel : ModelBase
{
    #region
    private readonly static BattleModel _inst = new BattleModel();
    static BattleModel() { }
    public static BattleModel Inst { get { return _inst; } }
    #endregion

    //自身
    public uint job { get; private set; }    //职业
    public FighterData selfData;

    //本局怪物
    private Dictionary<int, EnemyInstance> _dicEnemy = new Dictionary<int, EnemyInstance>();    //key: 实例ID

    //卡牌
    private List<CardInstance> _lstDeck = new List<CardInstance>(); //抽牌堆
    private List<CardInstance> _lstHand = new List<CardInstance>(); //手牌
    private List<CardInstance> _lstUsed = new List<CardInstance>(); //弃牌堆

    //数据统计
    public EffectStatistics effectStat;
    public RoundStatistics roundStat;
    public BattleStatistics battleStat;

    //费用
    public int curCost { get; private set; }
    public int maxCost { get; private set; }

    //全局
    public Bout bout { get; private set; }

    private BattleModel()
    {
        InitSelf();
    }

    public void InitBattle()
    {
        selfData.BattleInit();
        InitEnemy();
        InitStatistics();

        InitDeck();
        ShuffleDeck();
        _lstHand.Clear();

        _lstUsed.Clear();
        SendEvent(BattleEvent.USED_NUM_UPDATE);

        InitCost();
        SetBout(Bout.SELF);
    }

    //-------------------------自身数据-------------------------
    private void InitSelf()
    {
        job = Job.SWORD;    //todo 根据玩家选择指定
        selfData = new FighterData(1, 80, 80, 0); //todo  实例ID分配 根据职业初始化属性
    }

    /// <summary>
    /// 减少自身血量
    /// </summary>
    /// <param name="value"></param>
    public void ReduceSelfHp(int value)
    {
        selfData.ReduceHp(value);
        SendEvent(BattleEvent.SELF_HP_UPDATE, value);
        if(selfData.curHp <= 0)
            SendEvent(BattleEvent.SELF_DEAD);
    }

    /// <summary>
    /// 自身获得护甲
    /// </summary>
    /// <param name="value"></param>
    internal void AddArmor(int value)
    {
        selfData.armor += value;
        ArmorChangeHandle();
    }

    /// <summary>
    /// 更新护甲值
    /// </summary>
    /// <param name="value"></param>
    public void UpdateArmor(int value)
    {
        selfData.armor = value;
        ArmorChangeHandle();
    }

    private void ArmorChangeHandle()
    {
        SendEvent(BattleEvent.ARMOR_CHANGE);
    }

    private void InitCost()
    {
        curCost = maxCost = 3;
        CostChangeHandle();
    }

    /// <summary>
    /// 扣除费用
    /// </summary>
    /// <param name="iCost"></param>
    internal void ReduceCost(int iCost)
    {
        curCost -= iCost;
        if (curCost < 0)
        {
            Debug.LogError("cost can not be zero.");
            curCost = 0;
        }
        CostChangeHandle();
    }

    public void RecoverCost()
    {
        curCost = maxCost;
        CostChangeHandle();
    }

    private void CostChangeHandle()
    {
        SendEvent(BattleEvent.COST_CHANGE);
    }

    //-------------------------统计数据-------------------------
    public void InitStatistics()
    {
        effectStat = new EffectStatistics();
        roundStat = new RoundStatistics();
        battleStat = new BattleStatistics();
    }

    public void InitRoundStatistics()
    {
        effectStat = new EffectStatistics();
        roundStat = new RoundStatistics();
    }

    //-------------------------全局数据-------------------------

    public void SetBout(Bout value)
    {
        bout = value;
        SendEvent(BattleEvent.BOUT_UPDATE);
    }

    //-------------------------敌人数据-------------------------

    //初始化本局敌人
    private void InitEnemy()
    {
        _dicEnemy.Clear();
        _dicEnemy[0] = new EnemyInstance(1); //todo 根据关卡数据来初始化
        SendEvent(BattleEvent.ENEMY_INIT);
    }

    public Dictionary<int, EnemyInstance> GetEnemys()
    {
        return _dicEnemy;
    }

    public EnemyInstance GetEnemy(int instId)
    {
        if (_dicEnemy.ContainsKey(instId))
            return _dicEnemy[instId];
        return null;
    }

    /// <summary>
    /// 设置敌人下回合的行为
    /// </summary>
    /// <param name="instId"></param>
    /// <param name="boutAction"></param>
    internal void SetAction(int instId, BoutAction boutAction)
    {
        var enemyInst = GetEnemy(instId);
        if (enemyInst == null)
            return;
        enemyInst.boutAction = boutAction;
        SendEvent(BattleEvent.ENEMY_ACTION_UPDATE, instId);
    }

    /// <summary>
    /// 掉血
    /// </summary>
    /// <param name="instId"></param>
    /// <param name="iEffectValue"></param>
    internal void ReduceEnemyHp(int instId, int iEffectValue)
    {
        EnemyInstance enemyInstance = GetEnemy(instId);
        if (enemyInstance == null)
            return;
        enemyInstance.curHp -= iEffectValue;

        effectStat.damageLife += (uint)iEffectValue;
        roundStat.damageLife += (uint)iEffectValue;

        if (enemyInstance.curHp <= 0)
            enemyInstance.curHp = 0;
        SendEvent(BattleEvent.ENEMY_HP_UPDATE, new HpUpdateStruct(instId, -iEffectValue));
        if (enemyInstance.curHp == 0)
            SendEvent(BattleEvent.ENEMY_DEAD, instId);
    }

    /// <summary>
    /// 加护甲
    /// </summary>
    /// <param name="instId"></param>
    /// <param name="iEffectValue"></param>
    internal void AddEnemyArmor(int instId, int iEffectValue)
    {
        ReduceEnemyArmor(instId, -iEffectValue);
    }

    /// <summary>
    /// 掉护甲
    /// </summary>
    /// <param name="instId"></param>
    /// <param name="iEffectValue"></param>
    internal void ReduceEnemyArmor(int instId, int iEffectValue)
    {
        EnemyInstance enemyInstance = GetEnemy(instId);
        if (enemyInstance == null)
            return;

        enemyInstance.armor -= iEffectValue;

        effectStat.damageArmor += (uint)iEffectValue;
        roundStat.damageArmor += (uint)iEffectValue;

        if (enemyInstance.armor <= 0)
            enemyInstance.armor = 0;
    }
    //-------------------------卡牌数据-------------------------

    //根据收集到的牌初始化牌堆
    private void InitDeck()
    {
        _lstDeck.Clear();
        foreach (CardInstance collectCard in CharModel.Inst.GetCollectCardList())
        {
            _lstDeck.Add(collectCard.Clone());
        }
    }

    /// <summary>
    /// 洗牌
    /// </summary>
    public void ShuffleDeck()
    {
        System.Random random = new System.Random();
        _lstDeck.Sort(delegate (CardInstance a, CardInstance b)
        {
            return random.Next(-1, 2);
        }
        );
    }

    public List<CardInstance> GetDeckList()
    {
        return _lstDeck;
    }

    public List<CardInstance> GetHandList()
    {
        return _lstHand;
    }

    public List<CardInstance> GetUsedList()
    {
        return _lstUsed;
    }

    /// <summary>
    /// 抽一张牌
    /// </summary>
    internal void DrawOneCard()
    {
        //if (HasBuff(BuffType.CAN_NOT_DRAW_CARD))
        //{
        //    return;
        //}

        CardInstance drawCard = _lstDeck[0];
        if (drawCard == null)
        {
            Debug.LogError("no card for draw!");
            return;
        }
        _lstDeck.RemoveAt(0);
        SendEvent(BattleEvent.DECK_NUM_UPDATE);
        _lstHand.Add(drawCard);
        SendEvent(BattleEvent.DRAW_ONE_CARD, drawCard);
    }

    /// <summary>
    /// 从弃牌堆还原到抽牌堆并洗牌
    /// </summary>
    internal void ShuffleDeckFromUsed()
    {
        _lstDeck.Clear();
        _lstDeck.AddRange(_lstUsed);
        _lstUsed.Clear();
        ShuffleDeck();

        SendEvent(BattleEvent.DECK_NUM_UPDATE);
        SendEvent(BattleEvent.USED_NUM_UPDATE);
    }

    /// <summary>
    /// 将手牌移动到弃牌堆
    /// </summary>
    /// <param name="cardInstance"></param>
    internal void MoveHandCardToUsed(CardInstance cardInstance)
    {
        _lstHand.Remove(cardInstance);
        _lstUsed.Add(cardInstance);
        SendEvent(BattleEvent.MOVE_HAND_TO_USED, cardInstance);
        SendEvent(BattleEvent.USED_NUM_UPDATE);
    }

    /// <summary>
    /// 使用掉一张牌（不进去消耗区，本局无法再使用）
    /// </summary>
    /// <param name="cardInstance"></param>
    internal void ConsumeHandCard(CardInstance cardInstance)
    {
        _lstHand.Remove(cardInstance);
        SendEvent(BattleEvent.HAND_CARD_CONSUME, cardInstance);
    }

    /// <summary>
    /// 是否有对应类型的BUFF
    /// </summary>
    /// <returns></returns>
    internal bool HasBuff(uint buffType)
    {
        foreach (var buffInst in selfData.lstBuffInst)
        {
            BuffTemplate template = BuffTemplateData.GetData(buffInst.tplId);
            if (template == null)
                continue;
            if (template.nType == buffType)
                return true;
        }
        return false;
    }
    
    /// <summary>
    /// 自身获得buff
    /// </summary>
    /// <param name="buffId"></param>
    internal void AddSelfBuff(uint buffId)
    {
        BuffTemplate templet = BuffTemplateData.GetData(buffId);
        if (templet == null)
            return;
        bool hasBuff = false;
        foreach (var buffInst in selfData.lstBuffInst)
        {
            if (buffInst.tplId != buffId)
                continue;
            if (templet.iBout != -1)
                buffInst.leftBout += templet.iBout;
            buffInst.effectVal += templet.iEffectA;
            hasBuff = true;
            SendEvent(BattleEvent.SELF_BUFF_UPDATE, buffId);
            break;
        }
        if(!hasBuff)
        {
            selfData.lstBuffInst.Add(new BuffInst() { tplId = buffId, leftBout = templet.iBout, effectVal = templet.iEffectA });
            SendEvent(BattleEvent.SELF_BUFF_ADD, buffId);
        }
    }
    
}
