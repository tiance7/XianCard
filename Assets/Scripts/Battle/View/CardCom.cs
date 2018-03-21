using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace UI.Battle
{
    public partial class CardCom
    {
        private Vector2 _cardOrignPos = new Vector2();
        private CardInstance _cardInst;
        private CardTemplate _template;
        private Vector2 _tweenScaleHelper = new Vector2();
        private Vector2 _tweenMoveHelper = new Vector2();
        private int _orignChildIndex;

        //control
        private Tweener _tweenScale;
        private Tweener _tweenMove;

        public override void Dispose()
        {
            ReleaseControl();
            base.Dispose();
        }

        /// <summary>
        /// 设置卡牌信息
        /// </summary>
        /// <param name="drawCard"></param>
        internal void SetCard(CardInstance cardInst)
        {
            _cardInst = cardInst;
            _template = CardTemplateData.GetData(cardInst.tplId);
            if (_template == null)
                return;
            txtCost.text = _template.iCost.ToString();
            txtName.text = _template.szName;
            txtType.text = GetTypeDesc(_template.nType);
            txtDesc.text = _template.szDesc; //todo 处理通配符
            UpdateUsable();
        }

        public CardInstance GetCardInstance()
        {
            return _cardInst;
        }

        //获取卡牌类型描述
        private string GetTypeDesc(uint nType)
        {
            switch (nType)
            {
                case CardType.ATTACK:
                    return GameText.TYPE_ATTACK;
                case CardType.SKILL:
                    return GameText.TYPE_SKILL;
                case CardType.FORMATION:
                    return GameText.TYPE_FOMATION;
                default:
                    Debug.LogError("unhandle card type:" + nType);
                    return string.Empty;
            }
        }

        public Tweener UpdatePos(float newX, float newY, float moveTime = 0.5f)
        {
            _cardOrignPos.x = newX;
            _cardOrignPos.y = newY;
            return this.TweenMove(_cardOrignPos, moveTime);
        }

        public void UpdateUsable()
        {
            if (BattleModel.Inst.curCost >= _template.iCost)
                ctrlState.SetSelectedIndex((int)StateControl.CAN_USE);
            else
                ctrlState.SetSelectedIndex((int)StateControl.NO_COST);
        }

        /// <summary>
        /// 设置是否非战斗时被悬停选中
        /// </summary>
        /// <param name="isHolding"></param>
        internal void SetHoldNormal(bool isHolding)
        {
            if (this.parent == null)
                return;

            if (isHolding)
                _tweenScaleHelper.x = _tweenScaleHelper.y = 1.0f;
            else
                _tweenScaleHelper.x = _tweenScaleHelper.y = BattleDefine.CARD_SCALE;

            ReleaseTweenScale();
            _tweenScale = this.TweenScale(_tweenScaleHelper, 0.3f);
        }

        /// <summary>
        /// 设置是否战斗时被悬停选中
        /// </summary>
        /// <param name="isHolding"></param>
        internal void SetHold(bool isHolding, bool useTween = true, Action tweenEndCallback = null)
        {
            if (this.parent == null)
                return;

            if (isHolding)
            {
                _tweenScaleHelper.x = _tweenScaleHelper.y = 1.0f;
                _tweenMoveHelper.x = _cardOrignPos.x - this.width * (1 - BattleDefine.CARD_SCALE) / 2;
                _tweenMoveHelper.y = _cardOrignPos.y - (this.height - 206); //206为 HandCardCom的高度
                _orignChildIndex = this.parent.GetChildIndex(this);
                this.parent.SetChildIndex(this, this.parent.numChildren - 1);
            }
            else
            {
                _tweenScaleHelper.x = _tweenScaleHelper.y = BattleDefine.CARD_SCALE;
                _tweenMoveHelper.x = _cardOrignPos.x;
                _tweenMoveHelper.y = _cardOrignPos.y;
                this.parent.SetChildIndex(this, _orignChildIndex);
            }

            ReleaseTweenScale();
            ReleaseTweenMove();
            if (useTween)
            {
                _tweenScale = this.TweenScale(_tweenScaleHelper, 0.3f);
                _tweenMove = this.TweenMove(_tweenMoveHelper, 0.3f);
                if (tweenEndCallback != null)
                    _tweenMove.OnComplete(() => tweenEndCallback());
            }
            else
            {
                this.scale = _tweenScaleHelper;
                this.SetXY(_tweenMoveHelper.x, _tweenMoveHelper.y);
            }
        }

        private void ReleaseTweenScale()
        {
            if (_tweenScale != null)
            {
                _tweenScale.Kill();
                _tweenScale = null;
            }
        }

        private void ReleaseTweenMove()
        {
            if (_tweenMove != null)
            {
                _tweenMove.Kill();
                _tweenMove = null;
            }
        }

        private void ReleaseControl()
        {
            ReleaseTweenScale();
            ReleaseTweenMove();
        }

        internal void FlyToUsed(Vector2 usedCardPos)
        {
            ctrlState.SetSelectedIndex((int)StateControl.FLY_TO_USED);
            Tweener moveTween = UpdatePos(usedCardPos.x, usedCardPos.y, AnimationTime.HAND_TO_USED);
            moveTween.OnComplete(() => this.Dispose());
            tFlyToUsed.Play();
        }

        enum StateControl
        {
            CAN_USE,
            NO_COST,
            FLY_TO_USED
        }

    }
}