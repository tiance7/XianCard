using FairyGUI;
using UI.Battle;
using UnityEngine.SceneManagement;

public class BattleRewardWindow : WindowExtend
{    
    //view
    private BattleRewardFrame _frmBattleReward;

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

    private void InitView()
    {        
    }

    private void InitControl()
    {
        _frmBattleReward.btnJumpCard.onClick.Add(OnCloseClick);
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
}
