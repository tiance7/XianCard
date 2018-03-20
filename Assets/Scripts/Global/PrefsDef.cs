using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 定义到一起避免重复
/// </summary>
public class PrefsDef
{
    //用于储存关卡数据的游戏KEY
    public const string ONE_LINE = "oneLine";
    public const string SUDOKU = "sudoku";
    public const string COLOR_FILL = "colorFill";

    public const string GOLD = "gold";  //角色金币

    public const string MUTE = "mute";  //是否静音

    public const string LEVEL = "level";    //角色等级

    public const string EXP = "exp";    //当前经验

    public const string LAST_STAGE = "g{0}_m{1}";     //最新过关记录

    public const string LAST_MODE = "g{0}";     //最近玩过的难度

    public const string LANGUE_Id = "langue";     //语言代码

    public const string LAST_AD_TO_TIPS_TIME = "lastAdToTipsTime";     //最后登录时间

    //数独
    public const string SUDOKU_PALY_RECORD = "sudoku_play_record_{0}_{1}";     //数独关卡游戏记录
    public const string SUDOKU_HAS_RUN_GUIDE = "sudoku_has_run_guide";     //数独是否引导过了

    //提示次数
    public const string TIPS_CNT = "tipsCnt";    //当前提示次数
    public const string AD_TO_TIPS_CNT = "adToTipsCnt";    //广告转化提示次数

    //内购相关
    public const string HAS_IAP = "hasIap";  //是否内购过
    public const string IS_NO_AD = "isNoAd";  //是否无广告

    public const string DAILY_DATA = "dailyData";  //每日数据

    public const string LAST_REWARD_TIME = "lastRewardTime";       // 最后一次领取7日奖励时间
    public const string REWARD_DAY_COUNT = "rewardDayCount";       // 奖励累计时间
    public const string ONLINE_REWARD_TIME = "onlineRewardTime";   // 最后一次领取在线奖励时间

    //大厅
    public const string LB_LOGIN_RWD = "lbLoginRwd";        //大厅登录并给奖励
}
