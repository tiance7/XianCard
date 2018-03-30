using DG.Tweening;
using FairyGUI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Battle
{
    public partial class BattleFrame
    {
        private const int CARD_WIDTH = 250; //卡牌宽度 比实际稍小 让卡牌部分重叠显示
        private const int CARD_HEIGHT = 429; //卡牌高度
        private const int TIP_RENDER_WIDTH = 268;
        private const int TIP_RENDER_HEIGHT = 116;

        //model
        private BattleModel _battleModel;
        private float _skillUseY;   //鼠标小于这个高度后 判断技能卡被使用
        private Vector2 _usedCardPos;   //手牌飞入弃牌堆的坐标
        private List<TipStruct> _lstTip = new List<TipStruct>();

        //view
        private List<CardCom> _lstHandCard = new List<CardCom>();
        private CardCom _lastHoldCard;
        private TipList _comTipList;

        //control
        private BattleManager _manager;

        public override void ConstructFromResource()
        {
            base.ConstructFromResource();
            InitModel();
            InitView();
            InitControl();

            //开始战斗
            _battleModel.InitBattle();
        }

        public override void Dispose()
        {
            ReleaseControl();
            ReleaseTipList();
            base.Dispose();
        }

        private void InitModel()
        {
            _battleModel = BattleModel.Inst;

            _skillUseY = this.height - frmHand.height - CARD_HEIGHT / 2 + 50;   //50为修正偏移

            Vector2 globalPos = frmUsedCard.LocalToGlobal(Vector2.zero);
            _usedCardPos = frmHand.GlobalToLocal(globalPos);
        }

        private void InitView()
        {
            InitSelf();
            RefreshCost();
        }

        private void InitSelf()
        {
            ftSelf.InitFromSelf();
            RefreshSelfArmor();
        }

        private void RefreshSelfArmor()
        {
            int armor = _battleModel.selfData.armor;
            HpArmorControl armorControl = armor > 0 ? HpArmorControl.HAS : HpArmorControl.NO;
            ftSelf.pgsHp.ctrlArmor.SetSelectedIndex((int)armorControl);
            ftSelf.pgsHp.txtArmor.text = armor.ToString();
            if (armor > 0)
                ftSelf.pgsHp.tGetArmor.Play();
        }

        private void InitEnemy()
        {
            foreach (var kv in _battleModel.GetEnemys())
            {
                ftEnemy.Init(kv.Value); //todo 初始化多个敌人
                break;
            }
        }

        private void RefreshCost()
        {
            txtCost.text = string.Format("{0}/{1}", _battleModel.curCost, _battleModel.maxCost);
            foreach (CardCom cardCom in _lstHandCard)
            {
                cardCom.UpdateUsable();
            }
        }

        private void RefreshBuff()
        {
            ftSelf.SetBuffInstList(_battleModel.selfData.lstBuffInst);
        }

        private void InitControl()
        {
            _manager = new BattleManager();

            Stage.inst.onTouchMove.Add(OnTouchMove);

            btnEndTurn.onClick.Add(OnEndTurnClick);

            _battleModel.AddListener(BattleEvent.DRAW_ONE_CARD, OnDrawOneCard);
            _battleModel.AddListener(BattleEvent.MOVE_HAND_TO_USED, OnMoveHandToUsed);
            _battleModel.AddListener(BattleEvent.HAND_CARD_CONSUME, OnHandCardConsume);
            _battleModel.AddListener(BattleEvent.HAND_CARD_EXHAUST, OnHandCardConsume);
            _battleModel.AddListener(BattleEvent.ARMOR_CHANGE, OnArmorChange);
            _battleModel.AddListener(BattleEvent.COST_CHANGE, OnCostChange);
            _battleModel.AddListener(BattleEvent.ENEMY_INIT, OnEnemyInit);
            _battleModel.AddListener(BattleEvent.ENEMY_HP_UPDATE, OnEnemyHpUpdate);
            _battleModel.AddListener(BattleEvent.ENEMY_DEAD, OnEnemyDead);
            _battleModel.AddListener(BattleEvent.ENEMY_ACTION_UPDATE, OnActionUpdate);
            _battleModel.AddListener(BattleEvent.BOUT_UPDATE, OnBoutUpdate);
            _battleModel.AddListener(BattleEvent.SELF_HP_UPDATE, OnSelfHpUpdate);
            _battleModel.AddListener(BattleEvent.SELF_BUFF_ADD, OnSelfBuffAdd);
            _battleModel.AddListener(BattleEvent.SELF_BUFF_UPDATE, OnSelfBuffUpdate);
            _battleModel.AddListener(BattleEvent.SELF_BUFF_REMOVE, OnSelfBuffRemove);


            Message.AddListener(MsgType.DO_ATTACK, OnDoAttack);
            Message.AddListener(MsgType.SHOW_HIT_EFFECT, OnShowHitEffect);
            Message.AddListener(MsgType.BATTLE_WIN, OnBattleWin);
            Message.AddListener(MsgType.FIGHTER_ROLL_OVER, OnFighterRollOver);
            Message.AddListener(MsgType.FIGHTER_ROLL_OUT, OnFighterRollOut);
        }

        private void ReleaseControl()
        {
            if (_manager != null)
            {
                _manager.Dispose();
                _manager = null;
            }

            Stage.inst.onTouchMove.Remove(OnTouchMove);

            _battleModel.RemoveListener(BattleEvent.DRAW_ONE_CARD, OnDrawOneCard);
            _battleModel.RemoveListener(BattleEvent.MOVE_HAND_TO_USED, OnMoveHandToUsed);
            _battleModel.RemoveListener(BattleEvent.HAND_CARD_CONSUME, OnHandCardConsume);
            _battleModel.RemoveListener(BattleEvent.HAND_CARD_EXHAUST, OnHandCardConsume);
            _battleModel.RemoveListener(BattleEvent.ARMOR_CHANGE, OnArmorChange);
            _battleModel.RemoveListener(BattleEvent.COST_CHANGE, OnCostChange);
            _battleModel.RemoveListener(BattleEvent.ENEMY_INIT, OnEnemyInit);
            _battleModel.RemoveListener(BattleEvent.ENEMY_HP_UPDATE, OnEnemyHpUpdate);
            _battleModel.RemoveListener(BattleEvent.ENEMY_DEAD, OnEnemyDead);
            _battleModel.RemoveListener(BattleEvent.ENEMY_ACTION_UPDATE, OnActionUpdate);
            _battleModel.RemoveListener(BattleEvent.BOUT_UPDATE, OnBoutUpdate);
            _battleModel.RemoveListener(BattleEvent.SELF_HP_UPDATE, OnSelfHpUpdate);
            _battleModel.RemoveListener(BattleEvent.SELF_BUFF_ADD, OnSelfBuffAdd);
            _battleModel.RemoveListener(BattleEvent.SELF_BUFF_UPDATE, OnSelfBuffUpdate);
            _battleModel.RemoveListener(BattleEvent.SELF_BUFF_REMOVE, OnSelfBuffRemove);

            Message.RemoveListener(MsgType.DO_ATTACK, OnDoAttack);
            Message.RemoveListener(MsgType.SHOW_HIT_EFFECT, OnShowHitEffect);
            Message.RemoveListener(MsgType.BATTLE_WIN, OnBattleWin);
            Message.RemoveListener(MsgType.FIGHTER_ROLL_OVER, OnFighterRollOver);
            Message.RemoveListener(MsgType.FIGHTER_ROLL_OUT, OnFighterRollOut);
        }

        private void AddCardEvent(CardCom cardCom)
        {
            cardCom.onTouchBegin.Add(OnCardToucheBegin);
            cardCom.onTouchMove.Add(OnCardTouchMove);
            cardCom.onTouchEnd.Add(OnCardToucheEnd);
        }

        private void OnCardToucheBegin(EventContext context)
        {
            context.CaptureTouch();
            CardCom cardCom = context.sender as CardCom;
            cardCom.SetHold(true, false);
        }

        private void OnCardToucheEnd(EventContext context)
        {
            CardCom cardCom = context.sender as CardCom;
            if (TryUseCard(cardCom.GetCardInstance()))
                return;
            //卡没用掉 放回原处
            cardCom.SetHold(false, true, () =>
            {
                _lastHoldCard = null;
                UpdateHolderCard();
            });
        }

        private void OnCardTouchMove(EventContext context)
        {
            CardCom cardCom = context.sender as CardCom;
            Vector2 localPos = cardCom.parent.GlobalToLocal(Stage.inst.touchPosition);
            cardCom.SetXY(localPos.x - cardCom.width / 2, localPos.y - cardCom.height / 2);
        }

        //尝试使用卡牌
        private bool TryUseCard(CardInstance cardInstance)
        {
            if (cardInstance == null)
                return false;
            CardTemplate template = CardTemplateData.GetData(cardInstance.tplId);
            if (template == null)
                return false;
            if (_battleModel.curCost < template.iCost)  //如果费用不足,todo:根据卡牌实例里的费用
                return false;
            switch (template.nTargetType)
            {
                case CardTargetType.ONE_ENEMY:
                    //判断是否鼠标放置在某个敌人上面 是就对敌人使用
                    if (ftEnemy.rootContainer.HitTest(Stage.inst.touchPosition, true) == ftEnemy.rootContainer)
                    {
                        _manager.UseSkillCard(cardInstance, template, ftEnemy.instId);
                        //_battleModel.UseSkillCard(cardInstance, template, ftEnemy.instId);
                        return true;
                    }
                    break;
                case CardTargetType.SELF:
                case CardTargetType.ALL_ENEMY:
                case CardTargetType.NONE:
                    //判断是否超过某个高度 是就使用
                    if (Stage.inst.touchPosition.y < _skillUseY)
                    {
                        _manager.UseSkillCard(cardInstance, template);
                        //_battleModel.UseSkillCard(cardInstance, template);
                        return true;
                    }
                    break;
                default:
                    Debug.LogError("unhandle card targettype:" + template.nTargetType);
                    return false;
            }
            return false;
        }

        private void OnTouchMove(EventContext context)
        {
            UpdateHolderCard();
        }

        private void UpdateHolderCard()
        {
            if (GRoot.inst.touchTarget is CardCom)
            {
                CardCom holdCard = GRoot.inst.touchTarget as CardCom;
                if (_lastHoldCard == holdCard)
                    return;
                if (_lastHoldCard != null)
                    _lastHoldCard.SetHold(false);
                holdCard.SetHold(true);
                _lastHoldCard = holdCard;
                return;
            }

            if (_lastHoldCard != null)
            {
                _lastHoldCard.SetHold(false);
                _lastHoldCard = null;
            }
        }

        //回合结束点击
        private void OnEndTurnClick(EventContext context)
        {
            _battleModel.SetBout(Bout.ENEMY);
        }

        //抽一张卡
        private void OnDrawOneCard(object obj)
        {
            CardInstance drawCard = obj as CardInstance;
            CardCom cardCom = CardCom.CreateInstance();
            cardCom.scale = new Vector2(BattleDefine.CARD_DECK_SCALE, BattleDefine.CARD_DECK_SCALE);
            cardCom.x = -cardCom.width;
            cardCom.SetCard(drawCard);
            frmHand.AddChild(cardCom);
            _lstHandCard.Add(cardCom);
            AddCardEvent(cardCom);
            UpdateAllHandCardPos();
        }

        private void UpdateAllHandCardPos()
        {
            int cardNum = _lstHandCard.Count;
            float totalWidth = cardNum * CARD_WIDTH * BattleDefine.CARD_SCALE;
            if (totalWidth > frmHand.width)
                totalWidth = frmHand.width;
            float beginPos = (frmHand.width - totalWidth) / 2;
            float cardSpace = totalWidth / cardNum;
            for (int i = 0; i < cardNum; i++)
            {
                CardCom cardCom = _lstHandCard[i];
                cardCom.UpdatePos(beginPos + i * cardSpace, 0, BattleDefine.CARD_SCALE);
            }
        }

        //从手牌移动到弃牌堆
        private void OnMoveHandToUsed(object obj)
        {
            CardInstance cardInstance = obj as CardInstance;
            CardCom cardCom = GetCardCom(cardInstance);
            if (cardCom == null)
                return;
            _lstHandCard.Remove(cardCom);
            cardCom.FlyToUsed(_usedCardPos);
            UpdateAllHandCardPos();
        }

        private CardCom GetCardCom(CardInstance cardInstance)
        {
            foreach (CardCom cardCom in _lstHandCard)
            {
                if (cardCom.GetCardInstance().instId == cardInstance.instId)
                    return cardCom;
            }
            return null;
        }

        //手牌被永久使用掉
        private void OnHandCardConsume(object obj)
        {
            CardInstance cardInstance = obj as CardInstance;
            CardCom cardCom = GetCardCom(cardInstance);
            if (cardCom == null)
                return;
            _lstHandCard.Remove(cardCom);

            //todo 卡牌消耗动画
            cardCom.TweenFade(0, 1.0f).OnComplete(() => 
            {
                cardCom.Dispose();
                UpdateAllHandCardPos();
            });
        }

        private void OnArmorChange(object obj)
        {
            RefreshSelfArmor();
        }

        private void OnCostChange(object obj)
        {
            RefreshCost();
        }

        private void OnEnemyInit(object obj)
        {
            InitEnemy();
        }

        private Fighter GetFighter(int instId)
        {
            if (ftSelf.instId == instId)
                return ftSelf;
            return ftEnemy;
        }

        private void OnEnemyHpUpdate(object obj)
        {
            HpUpdateStruct hpUpdate = obj as HpUpdateStruct;
            EnemyInstance enemyInstance = _battleModel.GetEnemy(hpUpdate.instId);
            if (enemyInstance == null)
                return;
            Fighter fighter = GetFighter(hpUpdate.instId);
            if (fighter == null)
                return;
            fighter.UpdateHp(enemyInstance.curHp);
            if (hpUpdate.changeValue < 0)
            {
                fighter.ShowHitEffect();
                ShowFlyHpText(-hpUpdate.changeValue, fighter.xy);
            }
        }

        private void OnEnemyDead(object obj)
        {
            int instId = (int)obj;
            //todo 根据id获取Fighter
            ftEnemy.DeadHandle();
        }

        private void OnActionUpdate(object obj)
        {
            int instId = (int)obj;
            EnemyInstance enemyInstance = _battleModel.GetEnemy(instId);
            if (enemyInstance == null)
                return;
            Fighter fighter = GetFighter(instId);
            if (fighter == null)
                return;
            fighter.UpdateAction(enemyInstance.boutAction);
        }

        private void OnBoutUpdate(object obj)
        {
            btnEndTurn.enabled = _battleModel.bout == Bout.SELF;
            switch (_battleModel.bout)
            {
                case Bout.SELF:
                    comBout.txxBout.text = GameText.BATTLE_1;
                    break;
                case Bout.ENEMY:
                    comBout.txxBout.text = GameText.BATTLE_2;
                    break;
                default:
                    Debug.LogError("unhandle bout type:" + _battleModel.bout);
                    break;
            }
            comBout.tBout.Play();
        }

        private void OnSelfHpUpdate(object obj)
        {
            int reduceValue = (int)obj;
            ftSelf.UpdateHp(_battleModel.selfData.curHp);
            ShowFlyHpText(reduceValue, ftSelf.xy);
        }

        //显示飘血动画
        private void ShowFlyHpText(int reduceValue, Vector2 xy)
        {
            HpText hpText = HpText.CreateInstance();
            hpText.txtHp.text = reduceValue.ToString();
            hpText.xy = xy;
            AddChild(hpText);
            float fadeTime = 1.0f;
            hpText.TweenMoveX(hpText.x + 200, fadeTime);
            hpText.TweenMoveY(this.height, fadeTime).SetEase(BattleTool.hpCurve);
            hpText.TweenFade(0, 0.5f).SetDelay(0.8f);
            DOTween.To(() => hpText.txtHp.color, newColor => hpText.txtHp.color = newColor, Color.white, fadeTime)
                .SetUpdate(true)
                .SetTarget(this);
        }

        private void OnSelfBuffAdd(object obj)
        {
            RefreshBuff();
        }

        private void OnSelfBuffUpdate(object obj)
        {
            RefreshBuff();
        }

        private void OnSelfBuffRemove(object obj)
        {
            RefreshBuff();
        }

        private void OnDoAttack(object obj)
        {
            AttackStruct attackStruct = obj as AttackStruct;
            Fighter fighter = GetFighter(attackStruct.casterInst.instId);
            if (fighter == null)
                return;
            fighter.DoAttack(() => { Message.Send(MsgType.ATTACK_HIT, attackStruct); });
        }

        private void OnShowHitEffect(object obj)
        {
            AttackStruct attackStruct = obj as AttackStruct;
            //todo 判断攻击者是否是敌人
            ftSelf.ShowHitEffect();
            if (attackStruct.isBlock)
                ShowBlockText(ftSelf.xy);
        }

        /// <summary>
        /// 显示格挡文字
        /// </summary>
        private void ShowBlockText(Vector2 xy)
        {
            BlockText blockText = BlockText.CreateInstance();
            blockText.xy = xy;
            AddChild(blockText);
            float fadeTime = 2.0f;
            blockText.TweenMoveY(blockText.y - 200, fadeTime).SetEase(Ease.OutQuad);
            //blockText.TweenMoveX(blockText.x + 200, fadeTime);
            //blockText.TweenMoveY(blockText.y + 400, fadeTime).SetEase(BattleTool.hpCurve);
            blockText.TweenFade(0, fadeTime);
            blockText.TweenScale(new Vector2(0.8f, 0.8f), fadeTime).OnComplete(() => { blockText.Dispose(); });
        }

        private void OnBattleWin(object obj)
        {
            frmHand.touchable = false;
            tBattleEnd.Play();
            SoundTool.inst.PlaySoundEffect(ResPath.SFX_BATTLE_WIN);
            Core.Inst.StartCoroutine(ShowChooseCard());
        }

        private IEnumerator ShowChooseCard()
        {
            yield return new WaitForSeconds(AnimationTime.BATTLE_END_DEAD_TIME);

            Stage.inst.onTouchMove.Remove(OnTouchMove);
            WindowManager.Open(WindowId.BATTLE_REWARD);
        }

        private void OnFighterRollOver(object obj)
        {
            int fighterInstId = (int)obj;
            Fighter fighter = GetFighter(fighterInstId);
            if (fighter == null)
                return;
            ShowTip(fighterInstId, fighter);
        }

        private void OnFighterRollOut(object obj)
        {
            HideTip();
        }

        //显示战斗者提示
        private void ShowTip(int fighterInstId, Fighter fighter)
        {
            _lstTip.Clear();

            //回合描述
            BoutAction boutAction = BattleTool.GetBoutAction(fighterInstId);
            if (boutAction != null)
                _lstTip.Add(BattleTool.GetActionTip(boutAction));

            //todo buff描述

            if (_comTipList == null)
                InitTipList();
            _comTipList.visible = true;
            _comTipList.lstTip.numItems = _lstTip.Count;
            if (boutAction == null)  //如果是自己
                _comTipList.SetXY(fighter.x + fighter.width, (this.height - _lstTip.Count * TIP_RENDER_HEIGHT) / 2);
            else
                _comTipList.SetXY(fighter.x - TIP_RENDER_WIDTH, (this.height - _lstTip.Count * TIP_RENDER_HEIGHT) / 2);
        }

        //隐藏战斗者提示
        private void HideTip()
        {
            if (_comTipList != null)
                _comTipList.visible = false;
        }

        private void InitTipList()
        {
            _comTipList = TipList.CreateInstance();
            _comTipList.lstTip.itemRenderer = OnTipRender;
            AddChild(_comTipList);
        }

        private void ReleaseTipList()
        {
            if(_comTipList != null)
            {
                _comTipList.Dispose();
                _comTipList = null;
            }
        }

        private void OnTipRender(int index, GObject item)
        {
            TipRender render = item as TipRender;
            render.SetData(_lstTip[index]);
        }
    }

    enum HpArmorControl
    {
        NO,
        HAS
    }

}
