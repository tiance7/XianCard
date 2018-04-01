using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 怪物实例
/// </summary>
public class EnemyInstance : ObjectBase
{
    public EnemyTemplate template { get; private set; }
    public BoutAction boutAction;   //下一回合要执行的行为

    public EnemyInstance(uint tplId) : base(ObjectType.ENEMY)
    {
        template = EnemyTemplateData.GetData(tplId);
        if (template == null)
            return;
        curHp = maxHp = 55;  //todo 删除测试代码
        armor = 10;
        lstBuffInst.Add(new BuffInst(instId) { tplId = 4, leftBout = -1, effectVal = 0 }); //护甲不消失
        lstBuffInst.Add(new BuffInst(instId) { tplId = 7, leftBout = -1, effectVal = 3 }); //多重护甲
        //curHp = maxHp = template.iHp;
    }

}
