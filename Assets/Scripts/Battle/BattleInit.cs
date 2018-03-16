using System.Collections;
using System.Collections.Generic;
using UI.Battle;
using UnityEngine;

public class BattleInit : MonoBehaviour
{

    public AnimationCurve hpCurve;

    // Use this for initialization
    private void Awake()
    {
        BattleBinder.BindAll();
        BattleTool.hpCurve = hpCurve;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
