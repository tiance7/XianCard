using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : IDisposable
{
    //model
    private BattleModel _battleModel;

    //control
    private Dictionary<int, IEnemy> _dicEnemyAction = new Dictionary<int, IEnemy>();    //敌人行动管理

    public BattleManager()
    {
        _battleModel = BattleModel.Inst;
        InitControl();
    }

    public void Dispose()
    {
        ReleaseControl();
    }

    private void InitControl()
    {
        _battleModel.AddListener(BattleEvent.ENEMY_INIT, OnEnemyInit);
        _battleModel.AddListener(BattleEvent.BOUT_UPDATE, OnBoutUpdate);
        _battleModel.AddListener(BattleEvent.ENEMY_DEAD, OnEnemyDead);

        Message.AddListener(MsgType.ATTACK_HIT, OnAttackHit);
    }

    private void ReleaseControl()
    {
        if (_dicEnemyAction != null)
        {
            _dicEnemyAction.Clear();
            _dicEnemyAction = null;
        }

        _battleModel.RemoveListener(BattleEvent.ENEMY_INIT, OnEnemyInit);
        _battleModel.RemoveListener(BattleEvent.BOUT_UPDATE, OnBoutUpdate);
        _battleModel.RemoveListener(BattleEvent.ENEMY_DEAD, OnEnemyDead);

        Message.RemoveListener(MsgType.ATTACK_HIT, OnAttackHit);
    }

    //自己的回合抽牌
    private void SelfBoutDrawCard(int drawNum)
    {
        //抽5张牌
        Core.Inst.StartCoroutine(DrawCard(drawNum));
    }

    //抽多张牌
    private IEnumerator DrawCard(int drawNum)
    {
        for (int i = 0; i < drawNum; i++)
        {
            if (!BattleTool.IsDeckHasCard()) //如果没卡了
            {
                float shuffleTime = BattleTool.ShuffleDeckFromUsed();
                yield return new WaitForSeconds(shuffleTime);
            }
            DrawOneCard();
            yield return new WaitForSeconds(0.2f);
        }
    }

    //抽一张牌
    private void DrawOneCard()
    {
        _battleModel.DrawOneCard();
    }

    private void OnEnemyInit(object obj)
    {
        Dictionary<int, EnemyInstance> enemys = _battleModel.GetEnemys();
        foreach (var kv in enemys)
        {
            _dicEnemyAction.Add(kv.Key, EnemyActionReg.GetAction(kv.Value.template.nId));
        }
    }

    private void OnBoutUpdate(object obj)
    {
        switch (_battleModel.bout)
        {
            case Bout.SELF:
                SelfBoutDrawCard(5);
                _battleModel.RecoverCost();
                if(!HasBuff(BuffType.KEEP_ARMOR))
                _battleModel.UpdateArmor(0);
                UpdateEnemyAction();
                break;
            case Bout.ENEMY:
                SelfBoutEndHandle();
                Core.Inst.StartCoroutine(EnemyBoutHandle());
                break;
            default:
                Debug.LogError("unhandle bout:" + _battleModel.bout);
                break;
        }
    }

    /// <summary>
    /// 是否有对应类型的BUFF
    /// </summary>
    /// <param name="buffType"></param>
    /// <returns></returns>
    private bool HasBuff(uint buffType)
    {
        foreach (var buffInst in _battleModel.selfData.lstBuffInst)
        {
            BuffTemplate template = BuffTemplateData.GetData(buffInst.tplId);
            if (template == null)
                continue;
            if (template.nType == buffType)
                return true;
        }
        return false;
    }

    //自身回合结束处理
    private void SelfBoutEndHandle()
    {
        //回合结束buff结算
        foreach (var buffInst in _battleModel.selfData.lstBuffInst)
        {
            BuffTemplate template = BuffTemplateData.GetData(buffInst.tplId);
            if (template == null || template.nTrigger != BuffTriggerType.BOUT_END)
                continue;
            switch (template.nType)
            {
                case BuffType.ADD_ARMOR:
                    _battleModel.AddArmor(template.iEffectA);
                    break;
                default:
                    Debug.LogError("unhandle bout end buff:" + template.nType);
                    break;
            }
        }
    }

    //更新敌人下回合行为
    private void UpdateEnemyAction()
    {
        foreach (var kv in _dicEnemyAction)
        {
            _battleModel.SetAction(kv.Key, kv.Value.GetNextBoutAction());
        }
    }

    private void OnEnemyDead(object obj)
    {
        int instId = (int)obj;
        _dicEnemyAction.Remove(instId);
    }

    //敌方回合处理
    private IEnumerator EnemyBoutHandle()
    {
        //手牌飞入弃牌堆
        var handListCopy = new List<CardInstance>(_battleModel.GetHandList());
        foreach (var handCard in handListCopy)
        {
            _battleModel.MoveHandCardToUsed(handCard);
        }
        yield return new WaitForSeconds(AnimationTime.HAND_TO_USED);

        //回合标题展示时间
        yield return new WaitForSeconds(AnimationTime.BOUT_DISPLAY_TIME);

        //todo 结算buff效果

        //按顺序执行回合动作
        foreach (var kv in _battleModel.GetEnemys())
        {
            BoutAction boutAction = kv.Value.boutAction;
            if (boutAction == null)
                continue;
            float actionTime = HandleBoutAction(kv.Value, boutAction);
            yield return new WaitForSeconds(actionTime);
        }

        //都执行完了统一等待
        yield return new WaitForSeconds(AnimationTime.BOUT_END_TIME);

        _battleModel.SetBout(Bout.SELF);
    }

    //处理某个敌人的行动
    private float HandleBoutAction(EnemyInstance enemyInst, BoutAction boutAction)
    {
        switch (boutAction.enemyAction)
        {
            case EnemyAction.ATTACK:
                bool isBlock = _battleModel.selfData.armor >= boutAction.iValue;
                Message.Send(MsgType.DO_ATTACK, new AttackStruct(enemyInst, boutAction, isBlock));
                return AnimationTime.ATTACK_TIME;
            default:
                Debug.LogError("unhandle enemy action:" + boutAction.enemyAction);
                return 0.5f;    //容错用时间
        }
    }

    private void OnAttackHit(object obj)
    {
        //结算对自身的伤害
        AttackStruct attackStruct = obj as AttackStruct;
        int orignArmor = _battleModel.selfData.armor;
        int leftArmor = orignArmor - attackStruct.boutAction.iValue;
        if (leftArmor < 0)
        {
            _battleModel.UpdateArmor(0);
            _battleModel.ReduceSelfHp(-leftArmor);
        }
        else  //如果护甲有剩余
        {
            _battleModel.UpdateArmor(leftArmor);
        }
        Message.Send(MsgType.SHOW_HIT_EFFECT, attackStruct);

        HandleOnHitEffect(attackStruct.casterInst, orignArmor, attackStruct.boutAction.iValue);
    }

    //结算自身受击效果
    private void HandleOnHitEffect(EnemyInstance enemyInst, int orignArmor, int atkValue)
    {
        foreach (var buffInst in _battleModel.selfData.lstBuffInst)
        {
            BuffTemplate template = BuffTemplateData.GetData(buffInst.tplId);
            if (template == null || template.nTrigger != BuffTriggerType.ON_HIT)
                continue;
            switch (template.nType)
            {
                case BuffType.ARMOR_REFLECT:
                    int reflectValue = (int)Math.Round(Math.Min(orignArmor, atkValue) * 0.5f);
                    _battleModel.ReduceEnemyHp(enemyInst.instId, reflectValue);
                    break;
                default:
                    Debug.LogError("unhandle on hit buff type:" + template.nType);
                    break;
            }
        }
    }
}