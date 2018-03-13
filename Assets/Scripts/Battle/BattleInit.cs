using System.Collections;
using System.Collections.Generic;
using UI.Battle;
using UnityEngine;

public class BattleInit : MonoBehaviour
{

    // Use this for initialization
    private void Awake()
    {
        BattleBinder.BindAll();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
