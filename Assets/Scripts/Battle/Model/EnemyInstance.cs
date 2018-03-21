using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 怪物实例
/// </summary>
public class EnemyInstance
{
    public int instId;
    public int curHp;
    public int maxHp;
    public EnemyTemplate template { get; private set; }
    public BoutAction boutAction;   //下一回合要执行的行为

    public EnemyInstance(uint tplId)
    {
        template = EnemyTemplateData.GetData(tplId);
        if (template == null)
            return;
        curHp = maxHp = 1;  //todo 删除测试代码
        //curHp = maxHp = template.iHp;
    }

}
