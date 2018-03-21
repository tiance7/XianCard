using System;
using System.Collections.Generic;
using FairyGUI;
using UI.Battle;
using UnityEngine.SceneManagement;

public class BattleRewardWindow : WindowExtend
{
    //model
    private List<CardTemplate> _lstRewardCard;

    //view
    private BattleRewardFrame _frmBattleReward;
    private CardCom _lastHoldCard;

    public BattleRewardWindow()
    {
        modal = true;
        fitType = FitScreen.FullScreen;
    }

    protected override void OnInit()
    {
        base.OnInit();
        _frmBattleReward = contentPane as BattleRewardFrame;

        InitView();
        InitControl();
    }

    public override void Dispose()
    {
        ReleaseControl();
        base.Dispose();
    }

    private void InitView()
    {
        _lstRewardCard = BattleTool.GenerateRewardCards();
        _frmBattleReward.lstReward.numItems = 1;
    }

    private void InitControl()
    {
        Stage.inst.onTouchMove.Add(OnTouchMove);
        _frmBattleReward.lstReward.onClickItem.Add(OnRewardClick);
        _frmBattleReward.btnJumpCard.onClick.Add(OnCloseClick);
        _frmBattleReward.btnJumpSelectCard.onClick.Add(OnJumpSelectCard);
    }

    private void ReleaseControl()
    {
        Stage.inst.onTouchMove.Remove(OnTouchMove);
    }

    private void OnTouchMove(EventContext context)
    {
        if (GRoot.inst.touchTarget is CardCom)
        {
            CardCom holdCard = GRoot.inst.touchTarget as CardCom;
            if (_lastHoldCard == holdCard)
                return;
            if (_lastHoldCard != null)
                _lastHoldCard.SetHoldNormal(false);
            holdCard.SetHoldNormal(true);
            _lastHoldCard = holdCard;
            return;
        }

        if (_lastHoldCard != null)
        {
            _lastHoldCard.SetHoldNormal(false);
            _lastHoldCard = null;
        }
    }

    private void OnRewardClick(EventContext context)
    {
        //todo 判断奖励类型
        //如果是卡牌类型奖励
        SetRewardControl(RewardControl.SELECT_CARD);
        _frmBattleReward.card1.SetCard(new CardInstance(_lstRewardCard[0].nId));
        _frmBattleReward.card2.SetCard(new CardInstance(_lstRewardCard[1].nId));
        _frmBattleReward.card3.SetCard(new CardInstance(_lstRewardCard[2].nId));
        _frmBattleReward.tSelect3Card.Play();
    }

    private void SetRewardControl(RewardControl rewardControl)
    {
        _frmBattleReward.ctrlReward.SetSelectedIndex((int)rewardControl);
    }

    private void OnCloseClick(EventContext context)
    {
        CloseSelf();
    }

    private void CloseSelf()
    {
        WindowManager.Close(windowId);
        SceneManager.LoadScene(SceneName.MAP);
    }

    private void OnJumpSelectCard(EventContext context)
    {
        SetRewardControl(RewardControl.LIST);
    }



    enum RewardControl
    {
        LIST,
        SELECT_CARD
    }
}
