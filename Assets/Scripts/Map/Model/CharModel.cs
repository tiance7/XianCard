using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharModel : ModelBase
{

    #region
    private readonly static CharModel _inst = new CharModel();
    static CharModel() { }
    public static CharModel Inst { get { return _inst; } }
    #endregion

    private int _gold;  //灵石

    private List<CardInstance> _lstCollectCard = new List<CardInstance>(); //收集的卡牌
    private List<RelicBase> _lstCollectRelic = new List<RelicBase>(); //收集的遗物

    private CharModel()
    {
        InitCollectCard();
        InitCollectRelic();
    }

    /// <summary>
    /// 灵石
    /// </summary>
    public int gold
    {
        get { return _gold; }
        set
        {
            _gold = value;
            SendEvent(CharEvent.GOLD_CHANGE);
        }
    }

    public List<CardInstance> GetCollectCardList()
    {
        return _lstCollectCard;
    }

    //初始化拥有的卡牌
    private void InitCollectCard()
    {
        //初始5张攻击卡 5张防御卡
        List<uint> lstInitCardId = new List<uint> { 1, 1, 1, 1, 1, 3, 3, 3, 3, 3 };

        //lstInitCardId.Add(5);   //todo 测试用 五灵归宗
        //lstInitCardId.Add(7);   //todo 测试用 金甲阵
        //lstInitCardId.Add(9);   //todo 测试用 地脉阵
        lstInitCardId.Add(35);   //todo 测试用
        lstInitCardId.Add(37);   //todo 测试用
        lstInitCardId.Add(40);
        lstInitCardId.Add(41);
        lstInitCardId.Add(41);
        lstInitCardId.Add(41);
        lstInitCardId.Add(45);
        lstInitCardId.Add(17);
        lstInitCardId.Add(19);
        lstInitCardId.Add(48);
        lstInitCardId.Add(49);
        lstInitCardId.Add(50);
        lstInitCardId.Add(41);

        foreach (uint cardId in lstInitCardId)
        {
            _lstCollectCard.Add(new CardInstance(cardId));
        }
    }

    //添加一张卡
    public void AddCollectCard(CardInstance cardInst)
    {
        _lstCollectCard.Add(cardInst);
    }

    private void InitCollectRelic()
    {
        AddCollectRelic(RelicId.HUZANG_HUFU);
    }

    /// <summary>
    /// 获取法宝列表
    /// </summary>
    /// <returns></returns>
    public List<RelicBase> GetRelicList()
    {
        return _lstCollectRelic;
    }

    public void AddCollectRelic(uint iRelicId)
    {
        RelicBase newRelic = RelicFactory.CreateNewRelic(iRelicId);
        if (newRelic != null)
        {
            _lstCollectRelic.Add(newRelic);
            //todo:放在逻辑层执行以下事件逻辑
            newRelic.OnPutIntoRelicList();
            SendEvent(CharEvent.RELIC_ADD, newRelic);
        }
    }
}
